using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTemplateLoader : MonoBehaviour
{
    public static AlienTemplateLoader ins;
    private void Awake()
    {
        ins = this;
    }

    public AlienTemplate Jaotop;
    public AlienTemplate Qayrat;

    public AlienTemplate loadAlien(AlienSpecies species)
    {
        
        AlienTemplate tmp = null;
        switch(species)
        {
            
            case AlienSpecies.jaotop:
                tmp = Jaotop;
                
                break;
            case AlienSpecies.qayrat:
                tmp = Qayrat;
                break;

        }
        
        return tmp;
    }


}
