using SuperMap.Data;
using SuperMap.UI;
using System.Windows;
using System.Windows.Controls;

namespace SuperMapDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Workspace m_workspace;
        private MapControl m_mapControl;
        private SceneControl m_sceneControl;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            #region 加载二维地图
            m_workspace = new Workspace();
            m_workspace.Open(new WorkspaceConnectionInfo(@"D:\DevelopUtil\SuperMap\SampleData\City\Changchun.smwu"));
            m_mapControl = new MapControl();
            m_mapControl.Action = SuperMap.UI.Action.Pan;
            //必须设置
            m_mapControl.Map.Workspace = m_workspace;
            m_mapControl.Map.Open(m_workspace.Maps[0]);
            //m_mapControl.Map.Center = new Point2D(0, 0);
            //赋值给前端控件hostMapControl
            this.twoDMap.Child = m_mapControl;
            #endregion

            #region 加载三维地图
            m_workspace = new Workspace();
            m_workspace.Open(new WorkspaceConnectionInfo(@"D:\DevelopUtil\SuperMap\SampleData\City\Changchun.smwu"));
            m_sceneControl = new SceneControl(SuperMap.Realspace.SceneType.Globe);
            m_sceneControl.Action = Action3D.Pan;
            //必须设置
            m_sceneControl.Scene.Workspace = m_workspace;
            //m_sceneControl.Scene.Open(m_workspace.Scenes[0]);
            //赋值给前端控件hostMapControl
            this.threeDMap.Child = m_sceneControl;
            #endregion
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_mapControl != null)
            {
                m_mapControl.Map.Close();
                m_mapControl.Dispose();
                m_mapControl = null;
            }
            if (m_sceneControl != null)
            {
                m_sceneControl.Scene.Close();
                m_sceneControl.Dispose();
                m_sceneControl = null;
            }
            if (m_workspace != null)
            {
                m_workspace.Close();
                m_workspace.Dispose();
                m_workspace = null;
            }
        }

        private void TwoOrThreeDShow(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if(btn.Name == "showTwo")
            {
                this.twoDMap.Visibility = Visibility.Visible;
                this.threeDMap.Visibility = Visibility.Collapsed;
            }
            else if(btn.Name == "showThree")
            {
                this.twoDMap.Visibility = Visibility.Collapsed;
                this.threeDMap.Visibility = Visibility.Visible;
            }
            else
            {
                this.twoDMap.Visibility = Visibility.Visible;
                this.threeDMap.Visibility = Visibility.Visible;
            }
        }
    }
}
