using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Repositories;
using LiteDB;

namespace AppControleFinanceiroCurso.IoC
{
    public static  class DependencyInjectionConfig
    {

        public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LiteDatabase>(
                options =>
                {
                    return new LiteDatabase($"Filename={AppSettings.DatabasePath};Connection=Shared");
                });

            builder.Services.AddTransient<ITransactionRepositoy, TransactionRepositoy>();

            return builder;
        }
    }
}
