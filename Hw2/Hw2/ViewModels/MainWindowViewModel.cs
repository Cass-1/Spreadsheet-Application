namespace Hw2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Greeting = RunDistinctIntegers();
    }
    private string RunDistinctIntegers() // this is your method
    {
        return "test";
    }
    public string Greeting { get; set;}
}
