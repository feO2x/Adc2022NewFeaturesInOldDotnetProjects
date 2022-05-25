using System.Windows.Forms;
using Light.GuardClauses;

namespace OldApp.Common;

#nullable enable

public static class Bindings
{
    public static TControl AddOneWayBinding<TControl>(this TControl control,
                                                      string targetProperty,
                                                      object viewModel,
                                                      string viewModelProperty)
        where TControl : Control
    {
        control.MustNotBeNull();

        control.DataBindings.Add(new Binding(targetProperty,
                                             viewModel,
                                             viewModelProperty,
                                             false,
                                             DataSourceUpdateMode.Never));
        return control;
    }

    public static TControl AddOneWayBinding<TControl>(this TControl control,
                                                      string targetProperty,
                                                      object viewModel,
                                                      string viewModelProperty,
                                                      ConvertEventHandler convertValue)
        where TControl : Control
    {
        control.MustNotBeNull();
        convertValue.MustNotBeNull();

        var binding = new Binding(targetProperty,
                                  viewModel,
                                  viewModelProperty,
                                  false,
                                  DataSourceUpdateMode.Never);
        binding.Format += convertValue;
        control.DataBindings.Add(binding);

        return control;
    }

    public static TControl AddTwoWayBinding<TControl>(this TControl control,
                                                      string targetProperty,
                                                      object viewModel,
                                                      string viewModelProperty)
        where TControl : Control
    {
        control.MustNotBeNull();

        control.DataBindings.Add(new Binding(targetProperty,
                                             viewModel,
                                             viewModelProperty,
                                             false,
                                             DataSourceUpdateMode.OnPropertyChanged));
        return control;
    }
}