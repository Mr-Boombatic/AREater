using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorMonster : MonoBehaviour
{
    private GameObject Meat;
    public GameObject Monster;
    public Animator MonsterAnimator;

    public UIDocument UIDocument;
    private Label label;
    private int score = 0;
    void Start()
    {
        MonsterAnimator = GetComponent<Animator>();

        var root = UIDocument.rootVisualElement;
        label = root.Q<Label>("score");
    }

    bool isEating = true;

    void FixedUpdate()
    {
        if (Meat != null)
        {
            var dist = (Monster.transform.position - Meat.transform.position).magnitude;

            if (dist <= 0.3f && !isEating)
            {
                MonsterAnimator.SetTrigger("Eat");
                MonsterAnimator.ResetTrigger("Walk");
                Destroy(Meat, MonsterAnimator.GetCurrentAnimatorStateInfo(0).length);
                isEating = !isEating;
                score++;
                label.text = "Score: " + score.ToString();
                return;
            }

            if (dist > 0.3f)
            {
                MonsterAnimator.SetTrigger("Walk");
                transform.position = Vector3.MoveTowards(Monster.transform.position, Meat.transform.position, 0.04f);
                transform.LookAt(Meat.transform.position);
            }

        }
        else
        {
            MonsterAnimator.SetTrigger("Idle");
            Meat = GameObject.Find("Meat(Clone)");
            if (isEating)
                isEating = !isEating;
        }

    }
}
