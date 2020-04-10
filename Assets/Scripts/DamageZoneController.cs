using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class DamageZoneController : MonoBehaviour
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CollidedWithPlayer(collision))
            {
                collision.GetComponent<RubyController>().AddHealth(-.2f);
                collision.GetComponent<RubyController>().ReceiveAttack();
            }
        }

        private bool CollidedWithPlayer(Collider2D collision)
        {
            return (collision.GetComponent<RubyController>() != null);
        }

    }
}
