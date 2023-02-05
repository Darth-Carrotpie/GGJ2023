using System;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0414
//idea for upgrade: this could be composite, made out of generic MessagePage or smth like it, whithin which would contain isSet states and other things if needed;

//Naming Convention:
//1. Message parts must be named starting "With".
//2. Message part name must contain meaningfull 1-2 words of what the part is meant to contain.
//3. Meaningfull part must not contain abstracts, but rather concrete purposes. I.e. not "WithString", but "WithPlayerName".

public class GameMessage : BaseMessage {
    public static GameMessage Write() {
        return new GameMessage();
    }

    Vector2 _coordinates;
    private bool coordinatesSet;
    public Vector2 coordinates {
        get {
            if (coordinatesSet)
                return _coordinates;
            else throw new Exception("No <coordinates> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithCoordinates(Vector2 value) {
        _coordinates = value;
        coordinatesSet = true;
        return this;
    }
    private bool leavesAmountSet;
    public int leavesAmount {
        get {
            if (leavesAmountSet)
                return _leavesAmount;
            else throw new Exception("No leavesAmount was set before request for GameMessage: " + this);
        }
        set { _leavesAmount = value; }
    }
    private int _leavesAmount;
    public GameMessage WithLeavesAmount(int value) {
        _leavesAmount = value;
        leavesAmountSet = true;
        return this;
    }
    private bool dropletAmountSet;
    public int dropletAmount {
        get {
            if (dropletAmountSet)
                return _dropletAmount;
            else throw new Exception("No _dropletAmount was set before request for GameMessage: " + this);
        }
        set { _dropletAmount = value; }
    }
    private int _dropletAmount;
    public GameMessage WithDropletAmount(int value) {
        _dropletAmount = value;
        dropletAmountSet = true;
        return this;
    }
    private bool branchAmountSet;
    public int branchAmount {
        get {
            if (branchAmountSet)
                return _branchAmount;
            else throw new Exception("No _branchAmount was set before request for GameMessage: " + this);
        }
        set { _branchAmount = value; }
    }
    private int _branchAmount;
    public GameMessage WithBranchAmount(int value) {
        _branchAmount = value;
        branchAmountSet = true;
        return this;
    }
    private bool heartAmountSet;
    public int heartAmount {
        get {
            if (heartAmountSet)
                return _heartAmount;
            else throw new Exception("No _heartAmount was set before request for GameMessage: " + this);
        }
        set { _heartAmount = value; }
    }
    private int _heartAmount;
    public GameMessage WithHearthAmount(int value) {
        _heartAmount = value;
        heartAmountSet = true;
        return this;
    }
    private bool deltaSet;
    public int delta {
        get {
            if (deltaSet)
                return _delta;
            else throw new Exception("No _delta was set before request for GameMessage: " + this);
        }
        set { _delta = value; }
    }
    private int _delta;
    public GameMessage WithDelta(int value) {
        _delta = value;
        deltaSet = true;
        return this;
    }
    private bool potatoesAmountSet;
    public int potatoesAmount {
        get {
            if (potatoesAmountSet)
                return _potatoesAmount;
            else throw new Exception("No potatoesAmount was set before request for GameMessage: " + this);
        }
        set { _potatoesAmount = value; }
    }
    private int _potatoesAmount;
    public GameMessage WithPotatoesAmount(int value) {
        _potatoesAmount = value;
        potatoesAmountSet = true;
        return this;
    }
    Transform _playerTransform;
    private bool playerTransformSet;
    public Transform playerTransform {
        get {
            if (playerTransformSet)
                return _playerTransform;
            else throw new Exception("No <_playerTransform> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithPlayerTransform(Transform value) {
        _playerTransform = value;
        playerTransformSet = true;
        return this;
    }

    int _playerID;
    private bool playerIDSet;
    public int playerID {
        get {
            if (playerIDSet)
                return _playerID;
            else throw new Exception("No <_playerID> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithPlayerID(int value) {
        _playerID = value;
        playerIDSet = true;
        return this;
    }

    int _damage;
    private bool damageSet;
    public int damage {
        get {
            if (damageSet)
                return _damage;
            else throw new Exception("No <_damage> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithDamage(int value) {
        _damage = value;
        damageSet = true;
        return this;
    }

    int _health;
    private bool healthSet;
    public int health {
        get {
            if (healthSet)
                return _health;
            else throw new Exception("No <_health> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithHealth(int value) {
        _health = value;
        healthSet = true;
        return this;
    }

    //example to handle empty messages better
    private string _strMessage;
    public string strMessage {
        get {
            if (strMessageSet)
                return _strMessage;
            else throw new Exception("No strMessage was set before request for GameMessage: " + this);
        }
        set { _strMessage = value; }
    } //must not be type of bool (if bool needed, use int)
    private bool strMessageSet; // must be private bool
    public GameMessage WithStringMessage(string value) {
        strMessage = value;
        strMessageSet = true;
        return this;
    }

    Cycle _cycle;
    private bool cycleSet;
    public Cycle cycle {
        get {
            if (cycleSet)
                return _cycle;
            else throw new Exception("No <_cycle> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithCycle(Cycle value) {
        _cycle = value;
        cycleSet = true;
        return this;
    }

    Clickable _clickable;
    private bool clickableSet;
    public Clickable clickable {
        get {
            if (clickableSet)
                return _clickable;
            else throw new Exception("No <_clickable> was set before request for GameMessage: " + this);
        }
    }
    public GameMessage WithClickable(Clickable value) {
        _clickable = value;
        clickableSet = true;
        return this;
    }
}