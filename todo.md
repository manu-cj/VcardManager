- [ ] DisplayContacts()
- [ ] AddContact() - Implémentez une méthode pour ajouter un nouveau contact à la liste.
- [ ] SearchContactByName() - Ajoutez une fonctionnalité pour rechercher un contact par son nom.
- [ ] DeleteContact() - Créez une méthode pour supprimer un contact existant.
- [ ] ExportContact() - Implémentez une fonctionnalité pour exporter les contacts au format VCF.

**Fichiers principaux :**
- **Program.cs** : Gère le menu principal et les interactions utilisateur.
- **Contact.cs** : Définit les propriétés d'un contact (nom, téléphone, email).
- **ContactService.cs** : Implémente les fonctionnalités comme afficher, ajouter, rechercher, supprimer, et exporter des contacts.
- **FileService.cs** : Gère la lecture et l'écriture dans le fichier `contacts.vcf`.
- **InputValidator.cs** : Valide les entrées utilisateur (ex. email valide, champs non vides).