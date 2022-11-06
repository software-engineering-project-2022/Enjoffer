using EntityFrameworkCore.Triggered;

namespace ConsoleApp
{
    public class SetCalculatePages : IBeforeSaveTrigger<Book>
    {
        public Task BeforeSave(ITriggerContext<Book> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                context.Entity.NumberOfPages = context.Entity.BookContent.Length / 500;
            }

            return Task.CompletedTask;
        }
    }
}
