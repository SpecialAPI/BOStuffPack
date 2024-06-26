using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffectsTester : MonoBehaviour
{

    [Header("References (Do not modify)")]
    [SerializeField] Transform[] _EnemyLocations;
    [SerializeField] RectTransform _CharacterSpawnHolder;
    [SerializeField] RectTransform _CharacterFrontHolder;
    [SerializeField] RectTransform _CharacterBackHolder;
    [SerializeField] RectTransform _CharacterSwapHolder;
    [Header("TESTING Character Animation Controls")]
    [SerializeField] CharacterFieldEffectLayout _CharacterFieldEffectToSpawn;
    [SerializeField] bool _TheSlotHasACharacter = true;
    [SerializeField] bool _SpawnCharacterFieldEffect;
    [SerializeField] bool _RemoveAllCharacterFieldEffects;
    [Header("TESTING Enemy Animation Controls")]
    [SerializeField, Range(0,4)] int _EnemySlotPosition;
    [SerializeField] EnemyFieldEffectLayout _EnemyFieldEffectToSpawn;
    [SerializeField] bool _SpawnEnemyFieldEffect;
    [SerializeField] bool _RemoveAllEnemyFieldEffects;


    Dictionary<string, CharacterFieldEffectLayout> _CharacterFieldInstances = new Dictionary<string, CharacterFieldEffectLayout>();

    Dictionary<string, EnemyFieldEffectLayout> _EnemyFieldInstances = new Dictionary<string, EnemyFieldEffectLayout>();
    bool _hasUnit = true;
    // Update is called once per frame
    void Update()
    {
        if (_SpawnCharacterFieldEffect)
        {
            _SpawnCharacterFieldEffect = false;
            AddCharacterFieldEffect(_CharacterFieldEffectToSpawn);
        }
        else if (_RemoveAllCharacterFieldEffects)
        {
            _RemoveAllCharacterFieldEffects = false;
            RemoveAllCharacterFieldEffects();
        }
        else if (_hasUnit != _TheSlotHasACharacter)
        {
            _hasUnit = _TheSlotHasACharacter;
            UpdateAllCharacterFieldEffects();
        }
        else if (_SpawnEnemyFieldEffect)
        {
            _SpawnEnemyFieldEffect = false;
            AddEnemyFieldEffect(_EnemyFieldEffectToSpawn, _EnemyLocations[_EnemySlotPosition]);
        }
        else if (_RemoveAllEnemyFieldEffects)
        {
            _RemoveAllEnemyFieldEffects = false;
            RemoveAllEnemyFieldEffects();
        }
    }

    void AddCharacterFieldEffect(CharacterFieldEffectLayout prefab)
    {
        if (_CharacterFieldEffectToSpawn == null)
        {
            Debug.LogWarning("Add a character field effect!");
            return;
        }

        //DOES NOT HAVE LAYOUT, We create it
        if (!_CharacterFieldInstances.TryGetValue(prefab.name, out CharacterFieldEffectLayout layout))
        {
            layout = Instantiate(prefab, _CharacterSpawnHolder);
            layout.InitializeLayout(_CharacterFrontHolder, _CharacterBackHolder, _CharacterSwapHolder);
            _CharacterFieldInstances.Add(prefab.name, layout);
        }

        layout.AccessLayout(_hasUnit);
    }
    void RemoveAllCharacterFieldEffects()
    {
        foreach (CharacterFieldEffectLayout item in _CharacterFieldInstances.Values)
            item.EndAccessLayout();
    }
    void UpdateAllCharacterFieldEffects()
    {
        //Try Update all Field Effects!
        foreach (CharacterFieldEffectLayout item in _CharacterFieldInstances.Values)
        {
            if (item.IsActive)
                item.TryUpdateLayout(_hasUnit);
        }
    }

    void AddEnemyFieldEffect(EnemyFieldEffectLayout prefab, Transform location)
    {
        if (_EnemyFieldEffectToSpawn == null)
        {
            Debug.LogWarning("Add an enemy field effect!");
            return;
        }

        //DOES NOT HAVE LAYOUT, We create it
        if (!_EnemyFieldInstances.TryGetValue(prefab.name + location.name, out EnemyFieldEffectLayout layout))
        {
            layout = Instantiate(prefab, location);
            _EnemyFieldInstances.Add(prefab.name + location.name, layout);
        }

        layout.AccessLayout();
    }
    void RemoveAllEnemyFieldEffects()
    {
        foreach (EnemyFieldEffectLayout item in _EnemyFieldInstances.Values)
            item.EndAccessLayout();
    }
}
