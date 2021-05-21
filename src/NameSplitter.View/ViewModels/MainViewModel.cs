using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NameSplitter.Core;

namespace NameSplitter.View.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IServiceProvider _provider;

        public MainViewModel()
        {
            _provider = BuildServiceProvider();
            _ = _provider.GetRequiredService<NameSplitterDBContext>();
            TitleViewModel = new TitleViewModel(_provider);
            SalutationViewModel = new SalutationViewModel(_provider);
            NameViewModel = new NameViewModel(_provider);
            AnalyserViewModel = new AnalyserViewModel(_provider);
        }

        private IServiceProvider BuildServiceProvider()
        {
            var collection = new ServiceCollection();
            collection.AddDbContext<NameSplitterDBContext>(opt =>
            {
                opt.UseSqlite("Data Source=namesplitter.db");
            });
            collection.AddSingleton<NameSplitterAnalyser>();
            collection.AddSingleton<SalutationGenerator>();
            return collection.BuildServiceProvider();
        }

        public TitleViewModel TitleViewModel { get; }
        public SalutationViewModel SalutationViewModel { get; }
        public NameViewModel NameViewModel { get; }
        public AnalyserViewModel AnalyserViewModel { get; }
    }
}
