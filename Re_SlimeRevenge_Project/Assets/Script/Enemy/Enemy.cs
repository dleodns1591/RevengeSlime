using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public EEnemyType eEnemyType;
    public EMove eMove;

    public bool isKnockBack;
    public bool isCollsionAttack;

    public GameObject arrow;

    public GameObject bigBone;
    public GameObject smallBone;

    BaseEnemy baseEnemy;

    void Start()
    {
        baseEnemy = new BaseEnemy(this);
        baseEnemy.BaseEnemyType(eEnemyType);
    }

    void Update()
    {
        baseEnemy.BaseEnemyMove(eMove);
    }

    public T InstantiateSetParent<T>(T obj, Vector3 position, Quaternion rotation) where T : UnityEngine.Object
    {
        return Instantiate(obj, position, Quaternion.identity);
    }


    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {

        float waitTime = 0.5f;
        int playerInvincibility = 3;

        float bone = ((20 * baseEnemy.thisBase.bigBoneNum) + (10 * baseEnemy.thisBase.smallBoneNum));

        if (collision.CompareTag("Player"))
        {
            --baseEnemy.thisBase.hp;

            if (baseEnemy.thisBase.hp == 0)
            {
                eMove = EMove.Die;
                Player.Instance.eState = Player.EState.Eat;
                baseEnemy.spriteRenderer.DOFade(0, waitTime).SetEase(Ease.Linear);
                transform.DOScale(new Vector2(0.1f, 0.1f), waitTime).SetEase(Ease.Linear);
                transform.DORotate(new Vector3(0, 0, -180), waitTime).SetEase(Ease.Linear);


                Player.Instance.currentHp += bone + Player.Instance.getHP;
                Player.Instance.currentExperience += bone + Player.Instance.getExperience;

                yield return new WaitForSeconds(2f);
                Player.Instance.tag = "Player";
                Player.Instance.spriteRenderer.DOKill();
                Player.Instance.spriteRenderer.DOFade(1, 0);

                Player.Instance.eState = Player.EState.Walk;
                transform.DOKill();
                Destroy(gameObject);
            }

            else
            {
                //넉백
                eMove = EMove.BackMove;
                baseEnemy.rigidbody.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);

                if (isCollsionAttack == true)
                {
                    // 공격력 만큼 플레이어 체력 차감
                    Player.Instance.currentHp -= (baseEnemy.thisBase.attack - Player.Instance.defense);

                    // 무적 처리
                    Player.Instance.eState = Player.EState.Shock;
                    Player.Instance.tag = "invincibility";
                    Player.Instance.spriteRenderer.DOFade(0.5f, waitTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(playerInvincibility);

                    Player.Instance.spriteRenderer.DOKill();
                    Player.Instance.spriteRenderer.DOFade(1, 0);

                    Player.Instance.eState = Player.EState.Walk;
                    Player.Instance.tag = "Player";
                }
            }


        }

        if (collision.CompareTag("DestroyBox"))
            Destroy(gameObject);

        if (collision.CompareTag("Bomb"))
        {
            --baseEnemy.thisBase.hp;
            if (baseEnemy.thisBase.hp == 0)
            {
                transform.DOKill();
                Destroy(gameObject);

                if (baseEnemy.thisBase.bigBoneNum > 0)
                {
                    for (int i = 0; i < baseEnemy.thisBase.bigBoneNum; i++)
                    {
                        int randomRot = Random.Range(-180, 0);
                        Instantiate(bigBone, transform.position, Quaternion.Euler(0, 0, randomRot));
                    }
                }

                if (baseEnemy.thisBase.smallBoneNum + SkillManager.instance.skill[7].skillLevel > 0)
                {
                    for (int i = 0; i < baseEnemy.thisBase.smallBoneNum; i++)
                    {
                        int randomRot = Random.Range(-180, 0);
                        Instantiate(smallBone, transform.position, Quaternion.Euler(0, 0, randomRot));
                    }
                }
            }
            else
            {
                eMove = EMove.BackMove;
                baseEnemy.rigidbody.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);
            }
        }
    }
}
