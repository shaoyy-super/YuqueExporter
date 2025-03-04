using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace yuque_exporter
{
    public class Config
    {
        [JsonPropertyName("yuque")]
        public YuqueConfig Yuque { get; set; } = new YuqueConfig();
        [JsonPropertyName("dify")]
        public DifyConfig Dify { get; set; } = new DifyConfig();

        public class YuqueConfig
        {
            [JsonPropertyName("base_url")]
            public string BaseUrl { get; set; } = "https://www.yuque.com";

            [JsonPropertyName("groups")]
            public List<YuqueGroup> Groups { get; set; } = new List<YuqueGroup>();

            public void Validate()
            {
                if (string.IsNullOrEmpty(BaseUrl))
                {
                    throw new InvalidOperationException("语雀基础URL不能为空");
                }

                if (Groups == null || Groups.Count == 0)
                {
                    throw new InvalidOperationException("语雀分组配置不能为空");
                }

                foreach (var group in Groups)
                {
                    group.Validate();
                }
            }
        }

        public class YuqueGroup
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(Url))
                {
                    throw new InvalidOperationException("语雀分组URL不能为空");
                }

                if (string.IsNullOrEmpty(Token))
                {
                    throw new InvalidOperationException("语雀分组Token不能为空");
                }
            }
        }

        public class DifyConfig
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("dataset_id")]
            public string DatasetId { get; set; }

            [JsonPropertyName("api_key")]
            public string ApiKey { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(Url))
                {
                    throw new InvalidOperationException("Dify服务器URL不能为空");
                }

                if (string.IsNullOrEmpty(DatasetId))
                {
                    throw new InvalidOperationException("Dify数据集ID不能为空");
                }

                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new InvalidOperationException("Dify API密钥不能为空");
                }
            }
        }

        public static Config LoadConfig()
        {
            string configPath = Path.Combine(Environment.CurrentDirectory, "config.json");
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("配置文件不存在", configPath);
            }

            try
            {
                string jsonString = File.ReadAllText(configPath);
                var config = JsonSerializer.Deserialize<Config>(jsonString);
                if (config == null)
                {
                    throw new InvalidOperationException("配置文件解析失败");
                }

                // 验证配置
                config.Yuque.Validate();
                config.Dify.Validate();

                return config;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"配置文件格式错误: {ex.Message}", ex);
            }
            catch (Exception ex) when (ex is not InvalidOperationException && ex is not FileNotFoundException)
            {
                throw new InvalidOperationException($"加载配置文件时发生错误: {ex.Message}", ex);
            }
        }
    }
}