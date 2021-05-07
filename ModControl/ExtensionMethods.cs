using System.Windows.Forms;

namespace ModControl
{
    public static class ExtensionMethods
    {
        public static void EnableModDescriptionContextMenu(this RichTextBox richTextBox)
        {
            if (richTextBox.ContextMenuStrip == null)
            {
                // Create a ContextMenuStrip without icons
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.ShowImageMargin = false;

                // Add the Copy option (copies the selected text inside the richtextbox)
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Copy");
                toolStripMenuItem.Click += (sender, e) => richTextBox.Copy();
                contextMenuStrip.Items.Add(toolStripMenuItem);

                // When opening the menu, check if the condition is fulfilled 
                // in order to enable the action
                contextMenuStrip.Opening += (sender, e) =>
                {
                    toolStripMenuItem.Enabled = richTextBox.SelectionLength > 0;
                };

                richTextBox.ContextMenuStrip = contextMenuStrip;
            }
        }
    }
}
