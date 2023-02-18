using Unity.VisualScripting;
using UnityEngine;

namespace Game._Scripts.Player.Controller
{
    public class PlayerController : ControllerBase
    {
        private new void Update()
        {
            base.Update();
            
        }
        
        private new void LateUpdate()
        {
            base.LateUpdate();
            
        }
        protected override void MovementInput()
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }

        private void Animation()
        {
            
        }
    }
}
