﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Something that may become Atom
/// </summary>
[Serializable]
public class FutureAtom
    {

    [UnityEngine.SerializeField]
    private char letter;

    public char GetLetter()
    {
        return letter;
    }

    [UnityEngine.SerializeField]
    private List<Equation> equations;

    public List<Equation> GetEquations()
    {
        return equations;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="letter">Letter used to define atom.</param>
    /// <param name="equations">List of equations. According to them we can evaluate new Atom.</param>
    public FutureAtom(char letter, List<Equation> equations)
        {
            this.letter = letter;
            this.equations = equations;
        }

    /// <summary>
    /// Generates FutureAtom from String with proper pattern
    /// </summary>
    /// <param name="atomStr">String representation of future atom e.g. A(t1,t2+5.4,5)</param>
    /// <returns>Result FutureAtom or null if pattern doesnt match the string</returns>
    public static FutureAtom GenerateFutureAtomFromString(String atomStr)
    {
        if ((atomStr.Length > 1 && atomStr[1] == '(' && atomStr.Last() == ')'))
        {
            String[] equationsStrArray = atomStr.Substring(2, atomStr.Length - 3).Split(',');

            List<Equation> nParams = new List<Equation>();

            for (int i = 0; i < equationsStrArray.Length; i++)
            {
                nParams.Add(new Equation(equationsStrArray[i]));
            }

            return new FutureAtom(atomStr[0], nParams);
        }
        else if (atomStr.Length == 1)
        {
            return new FutureAtom(atomStr[0], new List<Equation>());
        }
        else return null;
    }
    /// <summary>
    /// Method evaluating new Atom
    /// </summary>
    /// <param name="args">Arguments that will be passed to Equations to evaluate new Atom parameters</param>
    /// <returns>created Atom</returns>
    public Atom evaluate(List<Double> args)
        {
            List<Double> nParameters = new List<double>();
            for (int i = 0; i < GetEquations().Count(); i++)
            {
                nParameters.Add(GetEquations()[i].apply(args));
            }

            return new Atom(this.GetLetter(), nParameters);
        }
    }
