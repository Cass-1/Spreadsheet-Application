namespace hw2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Greeting = RunDistinctIntegers();
    }
    private string RunDistinctIntegers() // this is your method
    {
        //TODO: make this method
        Application.Run();
    }
    public string Greeting { get; set;}
}