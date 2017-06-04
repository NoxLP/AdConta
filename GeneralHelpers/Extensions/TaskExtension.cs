using System;
using System.Threading.Tasks;

namespace Extensions
{
    public static class TaskExtension
    {
        //https://stackoverflow.com/questions/22864367/fire-and-forget-approach
        public static async Task Forget(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
