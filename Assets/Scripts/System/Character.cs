using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Character")]
[InlineEditor()]
public class Character : ScriptableObject
{
    public RequestDatabase requestDatabase;
    public Sprite sprite;
    public AnimationClip neutralAnimation;
    public AnimationClip neutralPositiveAnimation;
    public AnimationClip neutralNegativeAnimation;
    public AnimationClip hateAnimation;
    public AnimationClip loveAnimation;

    private const float LOVE = 0.8f;
    private const float SATISFIED = 0.6f;
    private const float NEUTRAL = 0.4f;
    private const float UNSATISFIED = 0.1f;
    private const float HATE = 0f;
    
    public List<string> loveResponseList = new();
    public List<string> satisfiedResponseList = new();
    public List<string> neutralResponseList = new();
    public List<string> unsatisfiedResponseList = new();
    public List<string> hateResponseList = new();
    public List<string> connectorsList = new();

    public string GetRandomConnector()
    {
        return GetRandom(connectorsList);
    }

    public AnimationClip GetResponseAnimation(Reaction _reaction)
    {
        switch (_reaction)
        {
            case Reaction.Love:
                return loveAnimation;
            case Reaction.Satisfied:
                return neutralPositiveAnimation;
            case Reaction.Hate:
                return hateAnimation;
            case Reaction.Unsatisfied:
                return neutralNegativeAnimation;
            default:
                return neutralAnimation;
        }
    }
    
    public Response GetResponse(float _score)
    {
        Response response = new();
        if (_score >= LOVE)
        {
            response.reaction = Reaction.Love;
            response.dialogue = GetRandom(loveResponseList);
        }
        else if (_score >= SATISFIED)
        {
            response.reaction = Reaction.Satisfied;
            response.dialogue = GetRandom(satisfiedResponseList);
        }
        else if (_score >= NEUTRAL)
        {
            response.reaction = Reaction.Neutral;
            response.dialogue = GetRandom(neutralResponseList);
        }
        else if (_score >= UNSATISFIED)
        {
            response.reaction = Reaction.Unsatisfied;
            response.dialogue = GetRandom(unsatisfiedResponseList);
        }
        else
        {
            response.reaction = Reaction.Hate;
            response.dialogue = GetRandom(hateResponseList);
        }

        return response;
    }

    public string GetRandom(List<string> _list)
    {
        if (_list.Count <= 0)
            return string.Empty;
        int random = Random.Range(0, _list.Count);
        return _list[random];
    }
}
