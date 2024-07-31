using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AppSecurityManager 
{
    public partial class AppMaintenanceWindow : Window
    {
        List<AppMaintenance> lstApps = DBRepository.GetApps();

        public AppMaintenanceWindow()
        {
            InitializeComponent();
            PopulateApplicationListView();
        }

        private void PopulateApplicationListView()
        {
            try
            {
                lstvw_Apps.Items.Clear();
                lstvw_Roles.Items.Clear();

                List<AppMaintenance> tempLstApps = lstApps;

                foreach (AppMaintenance app in tempLstApps)
                {
                    lstvw_Apps.Items.Add(app);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddUserVisibility();
            btn_delete.Visibility = Visibility.Hidden;
            btn_edit.Visibility = Visibility.Hidden;
            btn_SaveEdit.Visibility = Visibility.Hidden;
            btn_add.Visibility = Visibility.Hidden;
            btn_deleteapp.Visibility = Visibility.Hidden;

            lbl_appname.Visibility = Visibility.Hidden;
            btn_saveAdd.Visibility = Visibility.Hidden;
            // Call function that gets large list of all the apps so we can do linq queries for the rest of the lists

            PopulateApplicationListView();
        }

        private void lstvw_Apps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AppMaintenance s = (AppMaintenance)lstvw_Apps.SelectedItem;

                //If they select an app after selecting add app so the name bar to edit the name is shown
                btn_add.Visibility = Visibility.Visible;
                btn_SaveEdit.Visibility = Visibility.Visible;
                btn_addapp.Visibility = Visibility.Visible;
                btn_deleteapp.Visibility = Visibility.Visible;

                cmb_rolename.Visibility = Visibility.Hidden;
                cmb_sortOrder.Visibility = Visibility.Hidden;
                car_super.Visibility = Visibility.Hidden;
                lbl_role.Visibility = Visibility.Hidden;
                lbl_sortorder.Visibility = Visibility.Hidden;

                if (lstvw_Apps.SelectedIndex != -1)
                {
                    // populate the lstvw_Roles table

                    PopulateRoleListView(s);

                    switch (s.Type)
                    {
                        case "N":
                            car_apptype_Net.IsChecked = true;
                            break;
                        case "W":
                        default:
                            car_apptype_Web.IsChecked = true;
                            break;
                    }    

                    switch (s.roleRequired)
                    {
                        case true:
                            car_role.IsChecked = true;
                            break;
                        case false:
                            car_role.IsChecked = false;
                            break;
                    }

                    switch (s.delegation)
                    {
                        case true:
                            car_delegation.IsChecked = true;
                            break;
                        case false:
                            car_delegation.IsChecked = false;
                            break;

                    }
                }

                if (cmb_appname != null && s != null)
                {
                    cmb_appname.Text = s.Name;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        //Displays the roles of each app if the app has roles
        private void PopulateRoleListView(AppMaintenance sender)
        {
            try
            {
                lstvw_Roles.Items.Clear(); 

                foreach (AppRole a in sender.Roles)
                {
                    lstvw_Roles.Items.Add(a);

                }
                // change the display of buttons
                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
                lbl_appname.Visibility = Visibility.Hidden;
                btn_saveAdd.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lstvw_Roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btn_edit.Visibility = Visibility.Visible;
                AddUserVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddUserVisibility()
        {
            try
            {
                if (btn_edit.IsVisible)
                {
                    btn_delete.Visibility = Visibility.Visible;
                    btn_add.Visibility = Visibility.Hidden;
                    btn_SaveEdit.Visibility = Visibility.Visible;

                    car_super.Visibility = Visibility.Hidden;
                    lbl_role.Visibility = Visibility.Hidden;
                    lbl_sortorder.Visibility = Visibility.Hidden;
                    cmb_rolename.Visibility = Visibility.Hidden;
                    cmb_sortOrder.Visibility = Visibility.Hidden;

                    lbl_appname.Visibility = Visibility.Hidden;
                    btn_saveAdd.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Descending;

        private void GridViewColumnHeaderClicked(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(lstvw_Roles.Items);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        //Clears the options to fill the information to add a new role to the selected aplication
        private void btn_addrole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //This makes these elements visible on the UI
                lbl_role.Visibility = Visibility.Visible;
                lbl_sortorder.Visibility = Visibility.Visible;
                cmb_rolename.Visibility = Visibility.Visible;
                cmb_sortOrder.Visibility = Visibility.Visible;
                car_super.Visibility = Visibility.Visible;
                btn_add.Visibility = Visibility.Hidden;

                //Clears the selections to choose
                car_super.IsChecked = false;
                cmb_rolename.Clear();
                cmb_sortOrder.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //clears the options to add a new app and prompts a new bar for the app name
        private void btn_addapp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lstvw_Roles.Items.Clear();

                lbl_appname.Visibility = Visibility.Visible;
                btn_saveAdd.Visibility = Visibility.Visible;
                btn_SaveEdit.Visibility = Visibility.Hidden;
                btn_addapp.Visibility = Visibility.Hidden;
                btn_deleteapp.Visibility = Visibility.Hidden;

                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
                btn_add.Visibility = Visibility.Hidden;

                lbl_role.Visibility = Visibility.Hidden;
                lbl_sortorder.Visibility = Visibility.Hidden;
                cmb_rolename.Visibility = Visibility.Hidden;
                cmb_sortOrder.Visibility = Visibility.Hidden;
                car_super.Visibility = Visibility.Hidden;

                car_apptype_Web.IsChecked = false;
                car_role.IsChecked = false;
                car_delegation.IsChecked = false;
                car_apptype_Net.IsChecked = false;
                cmb_appname.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_saveapp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Generate a new GUID
                Guid newAppId = Guid.NewGuid();
                // Gather data from UI controls
                string appName = cmb_appname.Text.ToUpper();
                string appType = car_apptype_Web.IsChecked == true ? "W" : (car_apptype_Net.IsChecked == true ? "N" : "");
                bool roleRequired = car_role.IsChecked == true;
                bool delegation = car_delegation.IsChecked == true;

                if (car_apptype_Web.IsChecked != true && car_apptype_Net.IsChecked != true)
                {
                    MessageBox.Show("Select an app type");
                }
                else
                {
                    // Create a new instance of AppMaintenance
                    AppMaintenance newApp = new AppMaintenance
                    {
                        ID = newAppId,
                        Name = appName,
                        Type = appType,
                        roleRequired = roleRequired,
                        delegation = delegation,
                        Roles = new List<AppRole>()
                    };

                    // Add the new app to the database
                    DBRepository.AddApp(newApp);

                    lstApps.Add(newApp);
                    lstApps = DBRepository.GetApps();
                    PopulateApplicationListView();


                    car_apptype_Web.IsChecked = false;
                    car_apptype_Net.IsChecked = false;
                    car_role.IsChecked = false;
                    car_delegation.IsChecked = false;
                    cmb_appname.Text = "";
                    btn_addapp.Visibility = Visibility.Visible;
                    lbl_appname.Visibility = Visibility.Hidden;
                    btn_saveAdd.Visibility = Visibility.Hidden;
                    btn_deleteapp.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_deleteapp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppMaintenance selectedapp =(AppMaintenance)lstvw_Apps.SelectedItem;
                List<Selector> lstGroups = OracleDB.GetGroups();

                if (selectedapp.Roles.Count > 0 )
                {
                    MessageBox.Show("Role record found");
                }
                else
                {
                    // Confirm deletion
                    MessageBoxResult result = MessageBox.Show("App must not have user or group added to successfully delete the app." +
                        " Would you like to proceed?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        DBRepository.RemoveApplication(selectedapp);
                        lstApps = DBRepository.GetApps();
                        PopulateApplicationListView();

                        car_apptype_Web.IsChecked = false;
                        car_apptype_Net.IsChecked = false;
                        car_role.IsChecked = false;
                        car_delegation.IsChecked = false;
                        cmb_appname.Text = "";
                        btn_deleteapp.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the role" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Saves the modifications made to an existing app, saves the modification to an existing role and can also save a new role to a selected app
        private void Btn_SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Initialize objects to use
                AppMaintenance qApp = (AppMaintenance)lstvw_Apps.SelectedItem;
                AppRole qRole = (AppRole)lstvw_Roles.SelectedItem;

                //Updates to an existing application
                if (lstvw_Apps.SelectedIndex != -1)
                {
                    string appname = cmb_appname.Text.ToUpper();
                    bool delegation= car_delegation.IsChecked.Value; 
                    bool roleRequired = car_role.IsChecked.Value;

                    if (Convert.ToBoolean(car_apptype_Net.IsChecked))
                    {
                        qApp.Type = "N";
                    }
                    if (Convert.ToBoolean(car_apptype_Web.IsChecked))
                    {
                        qApp.Type = "W";
                    }

                    qApp.Name = appname;
                    qApp.roleRequired = roleRequired;
                    qApp.delegation = delegation;

                    DBRepository context = new DBRepository();
                    bool success = DBRepository.UpdateApplication(qApp);

                    lstvw_Roles.Items.Clear();
                    PopulateApplicationListView();

                    cmb_appname.Text = string.Empty;
                    car_apptype_Web.IsChecked = false;
                    car_apptype_Net.IsChecked = false;
                    car_role.IsChecked = false;
                    car_delegation.IsChecked = false;
                }
                // Update existing role 
                if (cmb_rolename.Text != "")
                {
                    string roleName = cmb_rolename.Text.ToUpper();
                    string sortOrderText = cmb_sortOrder.Text;
                    bool super = car_super.IsChecked.Value;
                    int.TryParse(sortOrderText, out int sortOrder);

                    // Update existing role
                    if (qRole != null)
                    {
                        qRole.Name = roleName;
                        qRole.sortOrder = sortOrder;
                        qRole.super = super;

                        DBRepository context = new DBRepository();
                        // Update role in the database

                        bool success = DBRepository.UpdateRole(qRole);

                        if (!success)
                        {
                            MessageBox.Show("Existing role name or sort order number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            lstApps = DBRepository.GetApps(); 
                            PopulateApplicationListView();
                        }
                    }
                    else
                    {
                        //Adds a new role
                        AppRole newRole = new AppRole
                        {
                            Name = roleName,
                            sortOrder = sortOrder,
                            super = super
                        };
                        // Add role to the local list and database
                        qApp.Roles.Add(newRole);
                        bool success = DBRepository.AddRole(qApp.ID, newRole);

                        if (!success)
                        {
                            MessageBox.Show("Existing role name or sort order number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            lstApps = DBRepository.GetApps();  // Get all the apps from the database
                            lstvw_Roles.Items.Clear();
                            PopulateApplicationListView();
                            return;
                        }
                    }
                }

                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
                btn_SaveEdit.Visibility = Visibility.Hidden;
                btn_add.Visibility = Visibility.Hidden;
                btn_deleteapp.Visibility = Visibility.Hidden;

                ////Refresh the roles list view
                lstvw_Roles.Items.Clear();
                return;
            }
            catch (Exception ex)
                 {
                MessageBox.Show("An error occurred while saving changes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        //Deletes a role from an application selected
        private void btn_deleterole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected role and app
                AppRole selectedRole = (AppRole)lstvw_Roles.SelectedItem;
                AppMaintenance selectedApp = (AppMaintenance)lstvw_Apps.SelectedItem;

                // Confirm deletion
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this role?","Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Attempt to remove the role from the database
                    DBRepository.RemoveRoleFromApp(selectedApp.ID, selectedRole);

                    selectedApp.Roles.Remove(selectedRole);
                    PopulateRoleListView(selectedApp);
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while deleting the role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btn_editRole_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                AppRole r = (AppRole)lstvw_Roles.SelectedItem;

                // Show the controls if a role is selected to edit
                lbl_role.Visibility = Visibility.Visible;
                lbl_sortorder.Visibility = Visibility.Visible;
                cmb_rolename.Visibility = Visibility.Visible;
                cmb_sortOrder.Visibility = Visibility.Visible;
                car_super.Visibility = Visibility.Visible;
                btn_add.Visibility = Visibility.Hidden;

                //Gets the data from the role and fills it
                car_super.IsChecked = r.super;
                cmb_rolename.Text = r.Name;
                cmb_sortOrder.Text = r.sortOrder.ToString();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            lstApps = DBRepository.GetApps();  // Get all the apps from the database
            lstvw_Roles.Items.Clear();

            car_apptype_Web.IsChecked = false;
            car_role.IsChecked = false;
            car_delegation.IsChecked = false;
            car_apptype_Net.IsChecked = false;
            btn_delete.Visibility = Visibility.Hidden;
            btn_edit.Visibility = Visibility.Hidden;
            btn_saveAdd.Visibility = Visibility.Hidden;
            lbl_appname.Visibility = Visibility.Hidden;
            btn_deleteapp.Visibility = Visibility.Hidden;
            btn_SaveEdit.Visibility = Visibility.Hidden;
            cmb_appname.Text = "";

            PopulateApplicationListView();
        }
    }
}
