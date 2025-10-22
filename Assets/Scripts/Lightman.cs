using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lightman : Character
{
    [Header("Attack Settings")]
    [SerializeField] private Bullet bullet;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    protected override void Skill()
    {
        if (Input.GetKeyDown(KeyCode.I) && isGround && !isAttack && !isDefend)
        {
            isAttack = true;
            ChangeAnim("Skill");
            StartCoroutine(WaitSkill());
            StartCoroutine(Fire());
        }
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.28f);
        float direction = Mathf.Sign(transform.localScale.x);
        Bullet newBullet = Instantiate(bullet);
        newBullet.MoveBullet(transform.position + Vector3.right * direction, direction);
    }

    public IEnumerator WaitSkill()
    {
        yield return new WaitForSeconds(0.7f);
        isAttack = false;
        speed = saveSpeed;
    }

    protected override void Defend()
    {
        if (Input.GetKeyDown(KeyCode.U) && isGround && !isAttack && !isDefend)
        {
            ChangeAnim("Defend");
            isDefend = true;
            speed = 0;
            Invoke(nameof(WaitPrepareToTeleport), 0.7f);
        }
    }

    public void WaitPrepareToTeleport()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        transform.position += new Vector3(3, 0, 0) * direction;
        StartCoroutine(Teleportationation());
    }

    public IEnumerator Teleportationation()
    {
        yield return new WaitForSeconds(1.64f);
        isDefend = false;
        speed = saveSpeed;
    }
}
