using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void Start()
    {
        // Sprawdzanie, czy obiekt ma rodzica
        if (transform.parent != null)
        {
            // Pobieranie komponentu Renderer rodzica
            Renderer parentRenderer = transform.parent.GetComponent<Renderer>();

            // Sprawdzanie, czy rodzic ma komponent Renderer
            if (parentRenderer != null)
            {
                // Pobieranie materia�u od rodzica
                Material parentMaterial = parentRenderer.material;

                // Pobieranie w�asnego komponentu Renderer
                Renderer ownRenderer = GetComponent<Renderer>();

                // Przypisywanie materia�u od rodzica do siebie
                if (ownRenderer != null)
                {
                    ownRenderer.material = parentMaterial;
                }
            }
        }

        particleSystem = GetComponent<ParticleSystem>();
        Invoke("DestroyObject", particleSystem.main.duration);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
