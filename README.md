# Prozess-Anzahl-Manager
Das Programm prüft ob ein angegebener Prozess öfters als erlaubt läuft und schließt diesen anhand der letzten Ausführung.
Diese Software kann z.B. genutzt werden, wenn man aus Lizenzgründen nur ein Programm gleichzeitig aufhaben darf.

Die Software ist Quelloffen und kann auch kommerziell genutzt werden.

# Installation des Prozess-Anzahl-Managers

1. Source-Code oder Release herunterladen

2. Verknüpfung zum Programm erstellen (Rechtsklick: Senden an Desktop) und die Verknüpfung in den Autostart-Ordner legen. Schneide dafür die Verknüpfung "Prozessanzahl.exe - Verknüpfung" aus und verschiebe diese
in den Autostart Ordner. Dazu drücke "Windowstaste + R" gleichzeitig, schreibe "%appdata%" in das Feld und klicke anschließend auf "OK".
Von dort aus gehe in den Ordner "Microsoft\Windows\Start Menü\Programme" und füge die Datei ein.

3. Anpassen der Startparameter: Mache anschl. Rechtsklick auf die Verknüpfung und klicke auf "Eigenschaften".
Von dort aus Bearbeiten Sie das Feld "Ziel". Ersetzen sie die Inhalte und belassen sie die Hochkommas. __"C:\Program Files\Prozessüberwachung\Prozessmanager.exe" 2 1500 "PROZESSNAME" "Das Programm darf nur 2 mal ausgeführt werden"__
Klicken Sie "OK" oder "Übernehmen" und öffnen Sie die Verknüpfung oder starten Sie das System neu.

# Start-Argumente zur Ausführung
1-Maximale Prozesse der selben Art (als Zahl)

2-Intervalprüfung - wie oft soll der Prozessbaum geprüft werden? (in Millisekunden)

3-Prozessname ohne Dateiendung - (bsp. "notepad.exe" = "notepad")

4-Fehlermeldung (Selbst definierbare Fehlermeldung)
