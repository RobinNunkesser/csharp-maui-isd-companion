using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCompanion;

public partial class BinaryAdditionPage : ContentPage
{
    readonly BinaryAdditionViewModel _viewModel;
    
    public BinaryAdditionPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new BinaryAdditionViewModel();
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