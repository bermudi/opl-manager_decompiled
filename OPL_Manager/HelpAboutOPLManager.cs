using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace OPL_Manager;

internal class HelpAboutOPLManager : Form
{
	private IContainer components;

	private TableLayoutPanel tableLayoutPanel;

	private PictureBox logoPictureBox;

	private Label labelProductName;

	private Label labelVersion;

	private Label labelCopyright;

	private Label labelCompanyName;

	private TextBox textBoxDescription;

	private Button okButton;

	public string AssemblyTitle
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
			if (customAttributes.Length != 0)
			{
				AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
				if (assemblyTitleAttribute.Title != "")
				{
					return assemblyTitleAttribute.Title;
				}
			}
			return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
		}
	}

	public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

	public string AssemblyDescription
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
		}
	}

	public string AssemblyProduct
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyProductAttribute)customAttributes[0]).Product;
		}
	}

	public string AssemblyCopyright
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
		}
	}

	public string AssemblyCompany
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
		}
	}

	public HelpAboutOPLManager()
	{
		InitializeComponent();
		((Control)this).Text = $"About {AssemblyTitle}";
		((Control)labelProductName).Text = AssemblyProduct;
		((Control)labelVersion).Text = $"Version {AssemblyVersion}";
		((Control)labelCopyright).Text = AssemblyCopyright;
		((Control)labelCompanyName).Text = AssemblyCompany;
		((Control)textBoxDescription).Text = AssemblyDescription;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Form)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Expected O, but got Unknown
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HelpAboutOPLManager));
		tableLayoutPanel = new TableLayoutPanel();
		logoPictureBox = new PictureBox();
		labelProductName = new Label();
		labelVersion = new Label();
		labelCopyright = new Label();
		labelCompanyName = new Label();
		textBoxDescription = new TextBox();
		okButton = new Button();
		((Control)tableLayoutPanel).SuspendLayout();
		((ISupportInitialize)logoPictureBox).BeginInit();
		((Control)this).SuspendLayout();
		tableLayoutPanel.ColumnCount = 2;
		tableLayoutPanel.ColumnStyles.Add(new ColumnStyle((SizeType)2, 33f));
		tableLayoutPanel.ColumnStyles.Add(new ColumnStyle((SizeType)2, 67f));
		tableLayoutPanel.Controls.Add((Control)(object)logoPictureBox, 0, 0);
		tableLayoutPanel.Controls.Add((Control)(object)labelProductName, 1, 0);
		tableLayoutPanel.Controls.Add((Control)(object)labelVersion, 1, 1);
		tableLayoutPanel.Controls.Add((Control)(object)labelCopyright, 1, 2);
		tableLayoutPanel.Controls.Add((Control)(object)labelCompanyName, 1, 3);
		tableLayoutPanel.Controls.Add((Control)(object)textBoxDescription, 1, 4);
		tableLayoutPanel.Controls.Add((Control)(object)okButton, 1, 5);
		((Control)tableLayoutPanel).Dock = (DockStyle)5;
		((Control)tableLayoutPanel).Location = new Point(10, 10);
		((Control)tableLayoutPanel).Margin = new Padding(4, 3, 4, 3);
		((Control)tableLayoutPanel).Name = "tableLayoutPanel";
		tableLayoutPanel.RowCount = 6;
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 10f));
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 10f));
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 10f));
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 10f));
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 50f));
		tableLayoutPanel.RowStyles.Add(new RowStyle((SizeType)2, 10f));
		((Control)tableLayoutPanel).Size = new Size(487, 307);
		((Control)tableLayoutPanel).TabIndex = 0;
		((Control)logoPictureBox).Dock = (DockStyle)5;
		logoPictureBox.Image = (Image)componentResourceManager.GetObject("logoPictureBox.Image");
		((Control)logoPictureBox).Location = new Point(4, 3);
		((Control)logoPictureBox).Margin = new Padding(4, 3, 4, 3);
		((Control)logoPictureBox).Name = "logoPictureBox";
		tableLayoutPanel.SetRowSpan((Control)(object)logoPictureBox, 6);
		((Control)logoPictureBox).Size = new Size(152, 301);
		logoPictureBox.SizeMode = (PictureBoxSizeMode)1;
		logoPictureBox.TabIndex = 12;
		logoPictureBox.TabStop = false;
		((Control)labelProductName).Dock = (DockStyle)5;
		((Control)labelProductName).Location = new Point(167, 0);
		((Control)labelProductName).Margin = new Padding(7, 0, 4, 0);
		((Control)labelProductName).MaximumSize = new Size(0, 20);
		((Control)labelProductName).Name = "labelProductName";
		((Control)labelProductName).Size = new Size(316, 20);
		((Control)labelProductName).TabIndex = 19;
		((Control)labelProductName).Text = "Product Name";
		labelProductName.TextAlign = (ContentAlignment)16;
		((Control)labelVersion).Dock = (DockStyle)5;
		((Control)labelVersion).Location = new Point(167, 30);
		((Control)labelVersion).Margin = new Padding(7, 0, 4, 0);
		((Control)labelVersion).MaximumSize = new Size(0, 20);
		((Control)labelVersion).Name = "labelVersion";
		((Control)labelVersion).Size = new Size(316, 20);
		((Control)labelVersion).TabIndex = 0;
		((Control)labelVersion).Text = "Version";
		labelVersion.TextAlign = (ContentAlignment)16;
		((Control)labelCopyright).Dock = (DockStyle)5;
		((Control)labelCopyright).Location = new Point(167, 60);
		((Control)labelCopyright).Margin = new Padding(7, 0, 4, 0);
		((Control)labelCopyright).MaximumSize = new Size(0, 20);
		((Control)labelCopyright).Name = "labelCopyright";
		((Control)labelCopyright).Size = new Size(316, 20);
		((Control)labelCopyright).TabIndex = 21;
		((Control)labelCopyright).Text = "Copyright";
		labelCopyright.TextAlign = (ContentAlignment)16;
		((Control)labelCompanyName).Dock = (DockStyle)5;
		((Control)labelCompanyName).Location = new Point(167, 90);
		((Control)labelCompanyName).Margin = new Padding(7, 0, 4, 0);
		((Control)labelCompanyName).MaximumSize = new Size(0, 20);
		((Control)labelCompanyName).Name = "labelCompanyName";
		((Control)labelCompanyName).Size = new Size(316, 20);
		((Control)labelCompanyName).TabIndex = 22;
		((Control)labelCompanyName).Text = "Company Name";
		labelCompanyName.TextAlign = (ContentAlignment)16;
		((Control)textBoxDescription).Dock = (DockStyle)5;
		((Control)textBoxDescription).Location = new Point(167, 123);
		((Control)textBoxDescription).Margin = new Padding(7, 3, 4, 3);
		((TextBoxBase)textBoxDescription).Multiline = true;
		((Control)textBoxDescription).Name = "textBoxDescription";
		((TextBoxBase)textBoxDescription).ReadOnly = true;
		textBoxDescription.ScrollBars = (ScrollBars)3;
		((Control)textBoxDescription).Size = new Size(316, 147);
		((Control)textBoxDescription).TabIndex = 23;
		((Control)textBoxDescription).TabStop = false;
		((Control)textBoxDescription).Text = "Description";
		((Control)okButton).Anchor = (AnchorStyles)10;
		okButton.DialogResult = (DialogResult)2;
		((Control)okButton).Location = new Point(395, 277);
		((Control)okButton).Margin = new Padding(4, 3, 4, 3);
		((Control)okButton).Name = "okButton";
		((Control)okButton).Size = new Size(88, 27);
		((Control)okButton).TabIndex = 24;
		((Control)okButton).Text = "&OK";
		((Form)this).AcceptButton = (IButtonControl)(object)okButton;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(7f, 15f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(507, 327);
		((Control)this).Controls.Add((Control)(object)tableLayoutPanel);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Margin = new Padding(4, 3, 4, 3);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "HelpAboutOPLManagerNew";
		((Control)this).Padding = new Padding(10, 10, 10, 10);
		((Form)this).ShowIcon = false;
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "HelpAboutOPLManagerNew";
		((Control)tableLayoutPanel).ResumeLayout(false);
		((Control)tableLayoutPanel).PerformLayout();
		((ISupportInitialize)logoPictureBox).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
