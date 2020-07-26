using UnityEngine;
using UnityEngine.Events;

public class FiringWeapon : Weapon
{
    [SerializeField]
    public int AmmoType;

    [SerializeField]
    public VacuumWeapon Vacuum;

    public UnityEvent OnFiringStart;

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();

        if (Input.GetAxis("Fire1") == 1 && Time.time > Lastfired && !Vacuum.IsWorking)
        {
            Lastfired = FireRate + Time.time;
            Shoot();
        }
    }

    public void Shoot()
    {
        IsoController.SwitchWeapon(0);
        OnFiringStart?.Invoke();

		BulletPool.Instance.Instantiate(AmmoType, IsoController.transform.position, IsometricPlayerMovementController.lastWantedDirection.normalized);
    }
}
