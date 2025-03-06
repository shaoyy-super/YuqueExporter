namespace yuque_exporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 开始执行文档导出任务...", ConsoleColor.Cyan);

                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在下载语雀文档...", ConsoleColor.Cyan);
                await YuqueDownloader.DownloadYuqueDoc();
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 语雀文档下载完成", ConsoleColor.Green);

                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 正在上传文档到Dify服务器...", ConsoleColor.Yellow);
                await DifyUploader.UploadToDify();
                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 文档上传完成", ConsoleColor.Green);

                DebugLog.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 所有任务执行完成", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 程序执行出错: {ex.Message}");
                DebugLog.LogError($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 错误详情: {ex}");
                Environment.Exit(1);
            }
        }
    }
}