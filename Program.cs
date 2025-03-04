using System;
using System.Threading.Tasks;

namespace yuque_exporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始执行文档导出任务...");

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在下载语雀文档...");
                await YuqueDownloader.DownloadYuqueDoc();
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 语雀文档下载完成");

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在上传文档到Dify服务器...");
                await DifyUploader.UploadToDify();
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文档上传完成");

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 所有任务执行完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 程序执行出错: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 错误详情: {ex}");
                Environment.Exit(1);
            }
        }
    }
}