namespace NameSplitter.View.ViewModels
{
    public class MainViewModel : BindableBase
    {

        public MainViewModel()
        {
            TitleViewModel = new TitleViewModel();
            SalutationViewModel = new SalutationViewModel();
            NameViewModel = new NameViewModel();
        }

        public TitleViewModel TitleViewModel { get; }
        public SalutationViewModel SalutationViewModel { get; }
        public NameViewModel NameViewModel { get; }
    }
}
