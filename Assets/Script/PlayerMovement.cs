
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;
    public int skor = 0;

    private Vector2 arahGerak;
    private string arahTerakhir = "down";

    public Animator animator;

    private bool isStunned = false;
    private bool canAttack = true;
    private bool isAttacking = false;

    public float knockbackForce = 4f;
    public float stunDuration = 0.5f;
    public float attackCooldown = 0.5f;

    private string animasiSekarang = "";

    void PlayAnimation(string nama)
    {
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
            return;
        }

        int stateHash = Animator.StringToHash("Base Layer." + nama);

        if (!animator.HasState(0, stateHash))
        {
            Debug.LogError("Animation state not found: " + nama);
            return;
        }

        if (animasiSekarang != nama)
        {
            animator.Play(stateHash, 0, 0f);
            animasiSekarang = nama;
        }
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();

        if (arahGerak.y > 0) arahTerakhir = "up";
        else if (arahGerak.y < 0) arahTerakhir = "down";
        else if (arahGerak.x < 0) arahTerakhir = "left";
        else if (arahGerak.x > 0) arahTerakhir = "right";
    }

    void Update()
    {
        if (isStunned || isAttacking) return;

        Vector3 arah = new Vector3(
            arahGerak.x,
            arahGerak.y,
            0
        );

        transform.position += arah * kecepatan * Time.deltaTime;

        if (arahGerak != Vector2.zero)
            PlayAnimation("play_" + arahTerakhir);
        else
            PlayAnimation("play_idle");
    }

    // ATTACK SPACE
    void OnAttack(InputValue value)
    {
        if (value.isPressed && canAttack && !isStunned)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        PlayAnimation("play_attack");

        // Tunggu sampai animasi attack selesai
        yield return null;

        float durasi = animator.GetCurrentAnimatorStateInfo(0).length /
                       animator.GetCurrentAnimatorStateInfo(0).speed;

        yield return new WaitForSeconds(durasi);

        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            skor++;

            FindFirstObjectByType<GameManager>().AmbilKoin();
            Debug.Log("Score : " + skor);
        }

        if (other.CompareTag("Enemy") && !isStunned)
        {
            Vector2 direction =
                ((Vector2)transform.position -
                (Vector2)other.transform.position).normalized;

            Camera.main.GetComponent<CameraFollow>().HitEffect();

            StartCoroutine(Hit(direction));

        }
    }

    IEnumerator Hit(Vector2 direction)
    {
        isStunned = true;
        canAttack = false;

        PlayAnimation("play_Shock");

        float timer = 0;

        while (timer < stunDuration)
        {
            transform.position +=
                (Vector3)(direction * knockbackForce * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        isStunned = false;
        canAttack = true;
        animasiSekarang = "";

    }
}
