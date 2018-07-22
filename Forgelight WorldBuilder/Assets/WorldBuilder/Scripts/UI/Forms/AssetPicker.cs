namespace WorldBuilder.UI
{
    using System;
    using Formats.Pack;
    using UWinForms.System.Drawing;
    using UWinForms.System.Windows.Forms;
    using Zenject;
    using Button = UWinForms.System.Windows.Forms.Button;
    using Form = UWinForms.System.Windows.Forms.Form;
    using ListBox = UWinForms.System.Windows.Forms.ListBox;
    using SizeGripStyle = UWinForms.System.Windows.Forms.SizeGripStyle;
    using TextBox = UWinForms.System.Windows.Forms.TextBox;

    public class AssetPicker : Form
    {
        [Inject] private AssetManager assetManager;
        private AssetType assetType;

        public delegate void AssetPickedEvent(AssetRef assetRef);
        public event AssetPickedEvent OnAssetPicked;

        // Working Data
        private ListBox box;
        private TextBox search;
        private AssetRef[] assets;

        public AssetPicker(AssetType assetType, string title, string ok = "Select", string cancel = "Cancel")
        {
            this.assetType = assetType;
            Text = title;
            SizeGripStyle = SizeGripStyle.Show;
            Size = new Size(300, 400);
            ControlBox = false;

            Button selectButton = new Button();
            selectButton.Text = ok;
            selectButton.Location = new Point(15, 370);
            selectButton.Click += OnSelectClicked;

            Button cancelButton = new Button();
            cancelButton.Text = cancel;
            cancelButton.Location = new Point(200, 370);
            cancelButton.Click += (sender, args) => Close();

            box = new ListBox();
            box.Location = new Point(12, 70);
            box.Size = new Size(265, 280);

            Label searchLabel = new Label();
            searchLabel.Text = "Search:";
            searchLabel.Location = new Point(10, 44);

            search = new TextBox();
            search.Location = new Point(71, 44);
            search.Size = new Size(206, 20);
            search.TextChanged += (sender, args) => UpdateAssetList();

            Controls.Add(selectButton);
            Controls.Add(cancelButton);
            Controls.Add(box);
            Controls.Add(search);
            Controls.Add(searchLabel);
        }

        private void OnSelectClicked(object sender, EventArgs e)
        {
            Close();
            OnAssetPicked?.Invoke((AssetRef)box.SelectedItem);
        }

        protected override void OnShown(EventArgs e)
        {
            UpdateAssetList();
            base.OnShown(e);
        }

        public void UpdateAssetList()
        {
            if (assets == null)
            {
                assets = assetManager.GetAssetsByType(assetType);
            }

            box.Items.Clear();

            foreach (AssetRef asset in assets)
            {
                if (asset.DisplayName.ToLower().Contains(search.Text.ToLower()))
                {
                    box.Items.Add(asset);
                }
            }
        }
    }
}