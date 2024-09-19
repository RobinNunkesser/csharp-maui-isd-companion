using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCompanion;

public partial class BinaryToDecimalPage : ContentPage
{
    readonly BinaryToDecimalViewModel _viewModel;
    
    public BinaryToDecimalPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new BinaryToDecimalViewModel();
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