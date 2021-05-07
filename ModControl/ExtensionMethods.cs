using System.Windows.Forms;

// Important ! Create the ExtensionMethods class as a "public static" class
public static class ExtensionMethods
{
    public static void EnableContextMenu(this RichTextBox rtb)
    {
        if (rtb.ContextMenuStrip == null)
        {
            // Create a ContextMenuStrip without icons
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.ShowImageMargin = false;

            // Add the Copy option (copies the selected text inside the richtextbox)
            ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Copy");
            tsmiCopy.Click += (sender, e) => rtb.Copy();
            cms.Items.Add(tsmiCopy);

            // When opening the menu, check if the condition is fulfilled 
            // in order to enable the action
            cms.Opening += (sender, e) =>
            {
                tsmiCopy.Enabled = rtb.SelectionLength > 0;
            };

            rtb.ContextMenuStrip = cms;
        }
    }
}