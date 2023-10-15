using UnityEngine;

namespace Sample
{
    public class SidebarUIController : MonoBehaviour
    {
        [SerializeField] private SidebarUI sidebarUI = null;

        public void OpenSidebar()
        {
            Admanager.Inst.SetActiveBannerAd(false);
            sidebarUI.OpenSidebar();
        }

        public void CloseSidebar()
        {
            sidebarUI.CloseSidebar();
        }
    }
}