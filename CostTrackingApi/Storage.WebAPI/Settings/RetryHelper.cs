using Npgsql;

namespace Storage.WebAPI.Settings
{
    public static class RetryHelper
    {
        public static void RetryConnection(Action action, int maxRetries = 3, TimeSpan retryInterval = default)
        {
            var retries = 0;
            var retry = true;

            while (retry)
            {
                try
                {
                    action();
                    retry = false;
                }
                catch (NpgsqlException ex) when (retries < maxRetries)
                {
                    retries++;
                    if (retryInterval != default)
                        System.Threading.Thread.Sleep(retryInterval);
                }
            }
        }
    }
}
