using Cinemachine;
using UnityEngine;

public class PlatformVehicle : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private GameObject triangle;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    private void Start()
    {
        TriangleTween();
        EventsManager.Instance.ActionModifyVehicle += ModifyVehicle;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionModifyVehicle -= ModifyVehicle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            if (vehicle != null) return;
            triangle.SetActive(false);
            EventsManager.Instance.OnCanModifyVehicle(true);
            vehicle = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            triangle.SetActive(true);
            EventsManager.Instance.OnCanModifyVehicle(false);
            vehicle = null;
        }
    }
    
    private void ModifyVehicle()
    {
        var position = transform.position;
        vehicle.transform.position = new Vector3(0,vehicle.transform.position.y,0) + position;
        vehicle.transform.rotation = transform.rotation;
    }

    private void TriangleTween()
    {
        LeanTween.moveY(triangle, triangle.transform.localPosition.y + 1f, .75f).setLoopPingPong(-1);
    }
}
