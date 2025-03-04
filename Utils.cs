using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace yuque_exporter
{
    internal class Utils
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        /// <summary>
        /// 发送 HTTP 请求
        /// </summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="method">HTTP 方法（如 GET、POST 等）</param>
        /// <param name="header">自定义请求头</param>
        /// <param name="requestBody">请求体（POST 或 PUT 时使用）</param>
        /// <returns>HTTP 响应内容</returns>
        public static async Task<string> SendHttpRequest(string url, HttpMethod method, Dictionary<string, string> header, object requestBody = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url), "URL不能为空");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "yuque-exporter/1.0");

            foreach (var keyValue in header)
            {
                _httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
            }

            string responseBody = null;
            int maxRetries = 5;
            int baseDelay = 1000; // 基础延迟1秒

            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在发送{method}请求到 {url}，第 {retry + 1} 次尝试");
                    
                    HttpResponseMessage response = await SendRequestWithMethod(method, url, requestBody);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求成功: {url}");
                        break;
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求失败: HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 错误详情: {errorContent}");

                        if ((int)response.StatusCode >= 500)
                        {
                            // 服务器错误，使用指数退避策略重试
                            int delayMilliseconds = (int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000);
                            await Task.Delay(delayMilliseconds);
                            continue;
                        }
                        else if ((int)response.StatusCode == 429)
                        {
                            // 请求过于频繁，等待更长时间
                            await Task.Delay(Math.Min((retry + 1) * 5000, 30000));
                            continue;
                        }
                        else
                        {
                            // 客户端错误，直接抛出异常
                            throw new HttpRequestException($"HTTP {(int)response.StatusCode}: {errorContent}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] HTTP请求异常: {ex.Message}");
                    if (retry == maxRetries - 1) throw;
                    
                    await Task.Delay((int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000));
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 请求超时");
                    if (retry == maxRetries - 1) throw;
                    
                    await Task.Delay((int)Math.Min(Math.Pow(2, retry) * baseDelay, 10000));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 未知异常: {ex.Message}");
                    throw;
                }
            }

            return responseBody;
        }

        private static async Task<HttpResponseMessage> SendRequestWithMethod(HttpMethod method, string url, object requestBody)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                return await _httpClient.PostAsync(url, content);
            }
            else if (method == HttpMethod.Delete)
            {
                return await _httpClient.DeleteAsync(url);
            }
            else
            {
                return await _httpClient.GetAsync(url);
            }
        }
    }
}
