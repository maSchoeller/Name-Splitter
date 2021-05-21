# Tests

## Unit Test
Beschreibung der Unit Tests, Aufzählung was getestet wird und auf Ergebnisse verweisen


## Testdaten

| Eingabe                                                      | Vorname (erwartet)     | Nachname (erwartet)              | Geschlecht     | Titel (erwartet, mit ; getrennt)                   | Sprache       |
| ------------------------------------------------------------ | ---------------------- | -------------------------------- | -------------- | -------------------------------------------------- | ------------- |
| Frau Sandra Berger                                           | "Sandra"               | "Berger"                         | "FEMALE"       |                                                    | "Deutsch"     |
| Herr Dr. Sandro Gutmensch                                    | "Sandro"               | "Gutmensch"                      | "MALE"         | "Dr."                                              | "Deutsch"     |
| Professor Heinrich Freiherr vom Wald                         | "Heinrich"             | "Freiherr vom Wald"              | "MALE"         | "Professor"                                        | "Deutsch"     |
| Mrs. Doreen Faber                                            | "Doreen"               | "Faber"                          | "FEMALE"       |                                                    | "Englisch"    |
| Mme. Charlotte Noir                                          | "Charlotte"            | "Noir"                           | "FEMALE"       |                                                    | "Französisch" |
| Esobar y Gonzales                                            |                        |                                  |                |                                                    | "Spanisch"    |
| Frau Prof. Dr. rer. nat. Maria von  Leuthäuser-Schnarrenberger | "Maria"                | "von Leuthäuser-Schnarrenberger" | "FEMALE"     | "Prof.; Dr. rer. nat"                              | "Deutsch"     |
| Herr Dipl. Ing. Max von Müller                               | "Max"                  | "von Müller"                     | "MALE"         | "Dipl. Ing."                                       | "Deutsch"     |
| Dr. Russwurm, Winfried                                       | "Winfried"             | "Russwurm"                       | "MALE"         | "Dr."                                              | "Deutsch"     |
| Herr Dr.-Ing. Dr. rer. nat. Dr. h.c.  mult. Paul Steffens    | "Paul"                 | "Steffens"                       | "MALE"         | "Dr.-Ing.; Dr. rer. nat.; Dr. h.c.  mult."         | "Deutsch"     |

 
## Testeingaben

| Eingabe                                                              | Generierte Briefanrede                                 |
| ---------------------------------------------------------------------| ------------------------------------------------------ |
| Frau Sandra  Berger                                                  | "Sehr geehrte Frau Sandra Berger"                      |
| Herr Dr. Sandro Gutmensch                                            |   |
| Professor Heinrich Freiherr vom Wald                                 |   |
| Mrs. Doreen Faber                                                    |   |
| Mme. Charlotte Noir                                                  |   |
| Esobar y Gonzales                                                    |   |
| Frau Prof. Dr. rer. nat. Maria von  Leuthäuser-Schnarrenberger       |   |
| Herr Dipl. Ing. Max von Müller                                       |   |
| Dr. Russwurm, Winfried                                               |   |
| Herr Dr.-Ing. Dr. rer. nat. Dr. h.c.  mult. Paul Steffens            |   |
