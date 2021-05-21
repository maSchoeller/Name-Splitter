# Tests

## Unit Test
Die Unitests sind im Projekt: ``NameSplitter.Tests`` dort Testet die Methode den Analyser mit dem unten stehen Datensätzen.

## Testdaten

| Eingabe                                                      | Vorname (erwartet)     | Nachname (erwartet)              | Geschlecht     | Titel (erwartet, mit ; getrennt)                   | Sprache       |
| ------------------------------------------------------------ | ---------------------- | -------------------------------- | -------------- | -------------------------------------------------- | ------------- |
| Frau Sandra Berger                                           | "Sandra"               | "Berger"                         | "FEMALE"       |                                                    | "Deutsch"     |
| Herr Dr. Sandro Gutmensch                                    | "Sandro"               | "Gutmensch"                      | "MALE"         | "Dr."                                              | "Deutsch"     |
| Professor Heinrich Freiherr vom Wald                         | "Heinrich"             | "Freiherr vom Wald"              | "MALE"         | "Professor"                                        | "Deutsch"     |
| Mrs. Doreen Faber                                            | "Doreen"               | "Faber"                          | "FEMALE"       |                                                    | "Englisch"    |
| Mme. Charlotte Noir                                          | "Charlotte"            | "Noir"                           | "FEMALE"       |                                                    | "Französisch" |
| Herr Prinz Lorenzo von Matterhorn|                           |"Lorenzo"               |"von Matterhorn"                  | "MALE"         | "Deutsch"    |
| Frau Prof. Dr. rer. nat. Maria von  Leuthäuser-Schnarrenberger | "Maria"                | "von Leuthäuser-Schnarrenberger" | "FEMALE"     | "Prof.; Dr. rer. nat"                              | "Deutsch"     |
| Herr Dipl. Ing. Max von Müller                               | "Max"                  | "von Müller"                     | "MALE"         | "Dipl. Ing."                                       | "Deutsch"     |
| Dr. Russwurm, Winfried                                       | "Winfried"             | "Russwurm"                       | "MALE"         | "Dr."                                              | "Deutsch"     |
| Herr Dr.-Ing. Dr. rer. nat. Dr. h.c.  mult. Paul Steffens    | "Paul"                 | "Steffens"                       | "MALE"         | "Dr.-Ing.; Dr. rer. nat.; Dr. h.c.  mult."         | "Deutsch"     |

 
