# MultipleEntryFormDemo
This app demonstrates how to set up a form with multiple fields with the same name (input elements with the same name attribute value)
so that when the form is submitted it sends arrays to the controller.

## Branches
- 0-Birds
  Contains a single Bird model and a form for entering two sets of Bird information which is stored in session storage.
- 1-Sightings
  Added a Sighting model which contains a list of Birds. The form lets a user enter signting information along with two sets of Bird information.
  
## TODO
- Add a database. The session storage has become too complex and doesn't provide a good example of how to handle objects with lists in a real applicatin.
- Refactor the input form and associated controllers so that additonal sets of Bird info can be added to a sighting.
- Add a drop-down to the input form for choosing Bird Order from this list:
  Struthioniformes — Cassowaries, Emus, Kiwis, Ostriches and Rheas

Galliformes — Grouse, Quails, Pheasants and Turkeys

Anseriformes — Ducks, Geese, and Swans

Psittaciformes — Macaws and Parrots

Strigiformes — Owls

Apodiformes — Hummingbirds

Coraciiformes — Kingfishers

Falconiformes — Hawks, Eagles, and Vultures

Gaviiformes — Loons

Piciformes — Woodpeckers

Charadriformes — Sandpipers and Seagulls

Ciconiiformes — Herons

Columbiformes — Doves and Pigeons

Passeriformes (Songbirds or Perching Birds) — Buntings, Cardinals, Catbirds, Chats, Chickadees, Crossbills, Crows, Dippers, Finches, Flycatchers, Goldfinches, Gnatcatchers, Gnateaters, Jays, Kinglets, Larks, Magpies, Martins, Mockingbirds, Nuthatches, Ovenbirds, Pipits, Pittas, Robins, Ravens, Shrikes, Sparrows, Starlings, Swallows, Tits, Thrushes, Waxwings, Weavers, Woodcreepers, Woodwarblers, Wrens, and Vireos.