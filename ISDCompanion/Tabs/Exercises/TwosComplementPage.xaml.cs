using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCompanion;

public partial class TwosComplementPage : ContentPage
{
    readonly TwosComplementViewModel _viewModel;
    
    public TwosComplementPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new TwosComplementViewModel();
    }
    
    private void Button_Clicked(object sender, EventArgs e)
    {
        SwitchSolution.IsToggled = false;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Initialize();
    }
}