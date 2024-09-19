using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCompanion;

public partial class DecimalToBinaryPage : ContentPage
{
    private readonly DecimalToBinaryViewModel _viewModel;

    public DecimalToBinaryPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new DecimalToBinaryViewModel();
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