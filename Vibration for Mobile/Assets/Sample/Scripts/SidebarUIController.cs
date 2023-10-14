using UnityEngine;

namespace Sample
{
    public class SidebarUIController : MonoBehaviour
    {
        [SerializeField] private SidebarUI sidebarUI = null;

        public void OpenSidebar()
        {
            sidebarUI.OpenSidebar();
        }

        public void CloseSidebar()
        {
            sidebarUI.CloseSidebar();
        }
    }
}