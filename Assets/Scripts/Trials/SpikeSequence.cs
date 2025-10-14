using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SpikeSequence : MonoBehaviour
{
    [SerializeField] private Spike spike1;
    [SerializeField] private Spike spike2;
    [SerializeField] private Spike spike3;

    public void PlaySequence()
    {
        // Можно задать любую последовательность через DOTween.Sequence
        Sequence seq = DOTween.Sequence();

        seq.Append(spike3.Attack(false));
        seq.Append(spike2.Attack(false));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(false));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(true));
        seq.Join(spike3.Attack(true));
        seq.AppendInterval(1f);

        seq.Append(spike2.Attack(true));
        seq.AppendInterval(1f);
        seq.Append(spike2.Attack(true));
        seq.Join(spike3.Attack(false));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(false));
        seq.Join(spike3.Attack(true));
        seq.AppendInterval(1f);
        seq.Append(spike2.Attack(false));
        seq.Join(spike3.Attack(false));
        seq.AppendInterval(3f);
        seq.Append(spike1.Attack(false));
        seq.Join(spike2.Attack(false));
        // Повторять бесконечно
        //seq.SetLoops(-1, LoopType.Restart);
    }
}

