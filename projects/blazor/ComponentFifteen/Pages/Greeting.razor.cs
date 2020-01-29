using Microsoft.AspNetCore.Components;

namespace ComponentFifteen.Pages
{
    public partial class Greeting
    {
        [Parameter]
        public string Message {get; set;}

        int currentCount = 1;

        void IncrementCount()
        {
            currentCount++;
        }
    }    
}
