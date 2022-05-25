using System;
using System.Windows.Forms;
using OldApp.Common;

namespace OldApp;

#nullable enable

public sealed partial class MainForm : Form
{
    public MainForm() => InitializeComponent();

    public MainForm(MainFormViewModel viewModel) : this() => ViewModel = viewModel;

    private MainFormViewModel? ViewModel { get; }

    protected override void OnLoad(EventArgs _)
    {
        if (ViewModel is null)
            return;

        ContactsListBox.AddOneWayBinding(nameof(ContactsListBox.DataSource),
                                         ViewModel,
                                         nameof(MainFormViewModel.Contacts));
        LoadButton.AddOneWayBinding(nameof(LoadButton.Enabled),
                                    ViewModel,
                                    nameof(MainFormViewModel.IsLoading),
                                    (_, e) => e.Value = !(bool) e.Value);
        ProgressBar.AddOneWayBinding(nameof(ProgressBar.Visible),
                                     ViewModel,
                                     nameof(MainFormViewModel.IsLoading));
    }

    private void LoadContacts(object _, EventArgs __) =>
        ViewModel?.LoadContactsAsync();
}