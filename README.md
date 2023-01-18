Video Games Development  


**Space Oddity**


Relazione Preliminare + Specifiche

Membri: Federico Meloni, Lorenzo Massidda, Michela Fratta

**Obiettivo del gioco:**


Sei un astronauta che deve entrare in un asteroide che sta per collidere con la Terra, piantare una bomba al suo interno e fuggire prima che esploda. Si esplora l’asteroide a zero gravità con dei propulsori.


**Principali dettagli tecnici:**

2D single player side scroller, con sprite e tilemaps, fisica più o meno realistica della gravità a zero g con movimento del personaggio a propulsione, particelle per la traccia della propulsione.

Useremo Tiled come level editor.

**Asset:**

Abbiamo intenzione di creare asset 2D da zero se possibile con l’aiuto di Michela Fratta. Per il resto possiamo prenderne da internet.


**Scene e livelli:**


Più scene intese come le “stanze” in cui il protagonista deve passare per raggiungere l’obiettivo. Ogni stanza deve essere attraversata dal protagonista. Ogni stanza contiene nemici e meccaniche di ostacolo diverse. L’intero gioco consiste in una prima parte dove si arriva al centro del meteorite e in una seconda parte dove si torna alla navicella; la seconda parte è a tempo.


**Gameplay:**


Il giocatore dovrà far attraversare le stanze a zero gravità utilizzando come mezzo un jetpack posizionato alle spalle. Il jetpack è in grado attivare dei razzi in ogni direzione, ruotando. Puoi aggrapparti alle superfici e saltare in direzione normale ad esse. Ci sono superfici metalliche dove si può camminare con stivali magnetici.


I livelli saranno caratterizzati da 3 tipi di ostacoli:



*   immobili: spuntoni, alieni che ti lanciano cose, ecc
*   mobili: creature aliene fluttuanti, frammenti di meteorite
*   gravitazionali: buchi neri, centri di gravità verso le pareti o inversi.

Il movimento dell’astronauta avviene in una simulazione di zero gravità, dentro le cavità di un enorme asteroide.


L’astronauta dovrà evitare vari ostacoli nel suo percorso fino al centro del meteorite quali superfici taglienti, distorsioni di gravità e vari alieni viventi all’interno.

![image](https://user-images.githubusercontent.com/25965198/112347671-5a855480-8cc7-11eb-8f89-102987584489.png)

    


Alcuni alieni posso mettere in ulteriore pericolo l’astronauta con il lancio di detriti o proiettili. Le distorsioni di gravità possono modificare il movimento dell’astronauta e sono segnalate dal background.


L’astronauta si può aggrappare alle superfici sicure per fermarsi per poi spingersi in una qualsiasi direzione lontana da essa.

![image](https://user-images.githubusercontent.com/25965198/112347715-640ebc80-8cc7-11eb-9c5f-60e034dae8bc.png)

    




Attraverso l’attivazione di uno strumento chiamato “stivali magnetici” l’astronauta potrà camminare sulle superfici ferrose dell’asteroide per evitare distorsioni di gravità o per avere maggior manovrabilità per schivare detriti.


![image](https://user-images.githubusercontent.com/25965198/112347743-6a049d80-8cc7-11eb-917c-edb3cf64e3e3.png)



**GitHub Repository:**


**<span style="text-decoration:underline;">https://github.com/VGD-Unica-2018-2019/vgd-project-space-oddity-0</span>**
