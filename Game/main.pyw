import json
import os
from re import *
from tkinter import messagebox

try:
    from Tkinter import *
except ImportError:
    from tkinter import *


class DialogCreator(Frame):
    def __init__(self, parent):
        Frame.__init__(self, parent, bg="#3c3d3d")
        self.parent = parent
        self.active_level = 0
        self.active_scene = -1
        self.active_dialog = -1
        self.levels = []
        self.DialogPrams = None
        # Load UI elements
        self.load_levels()
        self.make_widgets(self.parent)
        root.bind("<Return>", self.handleReturn)

    def make_widgets(self, parent):
        self.parent.configure(background="#3c3d3d")
        self.Entries = Frame(parent, name="entries", bg="#3c3d3d")
        self.Entries.grid(columnspan=4, sticky=(N, S, E, W))
        self.parent.grid_rowconfigure(0, weight=1)
        self.parent.grid_rowconfigure(1, weight=1)
        self.parent.grid_columnconfigure(1, weight=1)
        for x in range(1, 5):
            Grid.columnconfigure(self.Entries, x, weight=1, uniform='a')

        for idxL, jsonL in enumerate(self.levels):
            self.ButtonEntry(
                self.Entries, jsonL['level'], idxL, 1, self.set_level, "#d4ffdf" if self.active_level == idxL else "")

            if idxL == self.active_level:
                # SCENCES
                for idxS, jsonS in enumerate(jsonL['scenes']):
                    actS = "#d4ffdf" if self.active_scene == idxS else ""
                    if self.active_dialog >= 0 and jsonL['dialogs'][self.active_dialog]['scene'] == idxS:
                        actS = "#f1deff"
                    self.ButtonEntry(self.Entries, jsonS['name'], idxS, 2, self.set_scene, actS)
                NewScene = Text(self.Entries, height=2, width=2, bg='#d4fcff')
                NewScene.grid(row=len(jsonL['scenes']), column=2, sticky=(
                    N, S, E, W), padx=(8, 0), pady=(8, 0))
            # DIALOG
                if self.active_scene >= 0:
                    for DI, DV in enumerate(jsonL['scenes'][self.active_scene]['dialog']):
                        if DV < len(jsonL['dialogs']):
                            actD = "#d4ffdf" if self.active_dialog >= 0 and DI == jsonL['scenes'][self.active_scene]['dialog'].index(self.active_dialog) else ""
                            self.ButtonEntry(self.Entries, jsonL['dialogs'][DV]['line'], DI, 3, self.set_dialog, actD)
                    NewDialog = Button(self.Entries, height=2, width=2, text="Add Dialog", bg='#d4fcff', command=self.add_dialog)
                    NewDialog.grid(row=(len(jsonL['scenes'][self.active_scene]['dialog'])), column=3, sticky=(N, S, E, W), padx=(8, 0), pady=(8, 0))
            # RESPONSES
                for idxR, jsonR in enumerate(jsonL['responses']):
                    if self.active_dialog >= 0 and idxR == jsonL['dialogs'][self.active_dialog]['response']:
                        Response = Text(self.Entries, height=2, width=2, bg="#d4ffdf", wrap=WORD)
                    else:
                        Response = Text(self.Entries, height=2, width=2, bg="#f7f7f7", wrap=WORD)
                        Label(Response, text=str(idxR), height=2, fg="red", bg="#f7f7f7").pack(anchor="se")
                    Response.insert("1.0", jsonR)
                    Response.grid(row=idxR, column=4, sticky=(N, S, E, W), padx=(8, 8), pady=(8, 0))
                NewResponse = Text(self.Entries, height=2, width=2, bg='#d4fcff')
                NewResponse.grid(row=len(jsonL['responses']), column=4, sticky=(N, S, E, W), padx=(8, 8), pady=(8, 0))

        # Create the Box for editing dialog pramaters
        self.DialogPrams = Frame(parent, name="dialog_prams", bg="#5e5e5d")
        self.DialogPrams.grid(columnspan=4, rowspan=2, sticky=(S, E, W))
        for x in range(2, 6):
            Grid.columnconfigure(self.DialogPrams, x, weight=1, uniform='a')
        Grid.columnconfigure(self.DialogPrams, 1, weight=6)
        Label(self.DialogPrams, text="Line", bg="#8f8f8d").grid(
            row=0, column=1, sticky=(N, S, E, W), padx=(8, 0), pady=(8, 8))
        Label(self.DialogPrams, text="Attraction", bg="#8f8f8d").grid(
            row=0, column=2, sticky=(N, S, E, W), padx=(8, 0), pady=(8, 8))
        Label(self.DialogPrams, text="Uniqueness", bg="#8f8f8d").grid(
            row=0, column=3, sticky=(N, S, E, W), padx=(8, 0), pady=(8, 8))
        Label(self.DialogPrams, text="GoTo Scene", bg="#8f8f8d").grid(
            row=0, column=4, sticky=(N, S, E, W), padx=(8, 0), pady=(8, 8))
        Label(self.DialogPrams, text="Response", bg="#8f8f8d").grid(
            row=0, column=5, sticky=(N, S, E, W), padx=(8, 8), pady=(8, 8))
        if self.active_dialog >= 0 and self.active_dialog < len(self.levels[self.active_level]['dialogs']):
            dialog = self.levels[self.active_level]['dialogs'][self.active_dialog]
            DPLine = Text(self.DialogPrams, name="dialog_line",
                          height=2, width=2, bg="#d4ffdf")
            DPLine.insert("1.0", dialog['line'])
            DPLine.grid(row=2, column=1, sticky=(
                N, S, E, W), padx=(8, 0), pady=(0, 8))
            DPAttract = Text(
                self.DialogPrams, name="dialog_attraction", height=2, width=2, bg="#d4ffdf")
            DPAttract.insert("1.0", dialog['attarction'])
            DPAttract.grid(row=2, column=2, sticky=(
                N, S, E, W), padx=(8, 0), pady=(0, 8))
            DPUnique = Text(self.DialogPrams, name="dialog_uniqueness",
                            height=2, width=2, bg="#d4ffdf")
            DPUnique.insert("1.0", dialog['uniqueness'])
            DPUnique.grid(row=2, column=3, sticky=(
                N, S, E, W), padx=(8, 0), pady=(0, 8))
            DPResponse = Text(self.DialogPrams, name="dialog_scene", height=2, width=2, bg="#f1deff")
            DPResponse.insert("1.0", dialog['scene'])
            DPResponse.grid(row=2, column=4, sticky=(
                N, S, E, W), padx=(8, 0), pady=(0, 8))
            DPResponse = Text(
                self.DialogPrams, name="dialog_response", height=2, width=2, bg="#d4ffdf")
            DPResponse.insert("1.0", dialog['response'])
            DPResponse.grid(row=2, column=5, sticky=(
                N, S, E, W), padx=(8, 8), pady=(0, 8))

    def SavetoJson(self):
        # IF USER ENTERED IN TOP FIELD
        focus = str(root.focus_get())
        if '.entries.entry[3,' in focus:
            try:
                Prams = self.DialogPrams.grid_slaves(2, 1)
                Widget = self.Entries.grid_slaves(int(focus[-2]), 3)
                print(f"Inserting [{Widget[0].get('1.0', 'end-1c')}]")
                Prams[0].delete("1.0", END)
                Prams[0].insert("1.0", Widget[0].get("1.0", 'end-1c'))
            except IndexError:
                pass
        # LEVEL SAVE
        if self.ExtractJson(self.Entries, self.active_level, 1) is not None:
            self.levels[self.active_level]['level'] = self.ExtractJson(
                self.Entries, self.active_level, 1)
        # SCENES SAVE
        lenS = len(self.levels[self.active_level]['scenes'])
        for idxS in range(lenS):
            if self.ExtractJson(self.Entries, idxS, 2) is not None:
                self.levels[self.active_level]['scenes'][idxS]['name'] = self.ExtractJson(
                    self.Entries, idxS, 2)
        try:
            Widget = self.Entries.grid_slaves(lenS, 2)
            if isinstance(Widget[0], Text) and Widget[0].get("1.0", 'end-1c'):
                self.parse_import(Widget[0].get("1.0", 'end-1c'))
        except IndexError:
            pass
        # DIALOGS SAVE
        if (self.DialogPrams is not None and self.active_dialog >= 0 and
                self.active_dialog in self.levels[self.active_level]['scenes'][self.active_scene]['dialog']):
            for idxKD, jsonKD in enumerate(["line", "attarction", "uniqueness", "scene", "response"]):
                if self.ExtractJson(self.DialogPrams, 2, idxKD + 1) is not None:
                    self.levels[self.active_level]['dialogs'][self.active_dialog][jsonKD] = self.ExtractJson(
                        self.DialogPrams, 2, idxKD + 1)
        # RESPONSES SAVE
        lenR = len(self.levels[self.active_level]['responses'])
        for idxR in range(lenR):
            if self.ExtractJson(self.Entries, idxR, 4) is not None:
                self.levels[self.active_level]['responses'][idxR] = self.ExtractJson(
                    self.Entries, idxR, 4)
        if self.ExtractJson(self.Entries, lenR, 4) is not None:
            self.levels[self.active_level]['responses'].append(
                self.ExtractJson(self.Entries, lenR, 4))

    def ExtractJson(self, parent, row, col):
        try:
            Widget = parent.grid_slaves(row, col)
            if isinstance(Widget[0], Text) and Widget[0].get("1.0", 'end-1c'):
                text = Widget[0].get("1.0", 'end-1c').replace('\n', '').strip()
                try:
                    return int(text)
                except ValueError:
                    return text
        except IndexError:
            print(f"[{row}-{col}] Error fetching from: {parent}")
            return None

    def ButtonEntry(self, parent, text, row, col, command, bg=""):
        if bg:
            BE = Text(parent, name=f"entry[{col},{row}]", height=2, width=2, bg=bg, wrap=WORD)  # "#d4ffdf"
            BE.insert("1.0", text)
        else:
            BE = Button(parent, height=2, text=text,
                        command=lambda rw=row: command(rw))
            # Label(BE, text=str(row), height=2, fg="red", bg="#f7f7f7").place(anchor="se")
        BE.grid(row=row, column=col, sticky=(
            N, S, E, W), padx=(8, 0), pady=(8, 0))
        return BE

    def none(self, pram):
        print(pram)

    def set_level(self, level):
        self.SavetoJson()
        self.save_level()
        self.active_level = level
        self.active_scene = 0
        self.active_dialog = -1
        self.refresh_UI()
        x, y = root.winfo_pointerxy()
        widget = root.winfo_containing(x, y)
        widget.focus()

    def set_scene(self, scene):
        self.SavetoJson()
        self.save_level()
        self.active_scene = scene
        self.active_dialog = -1
        self.refresh_UI()
        x, y = root.winfo_pointerxy()
        widget = root.winfo_containing(x, y)
        widget.focus()

    def set_dialog(self, dialog):
        self.SavetoJson()
        self.save_level()
        self.active_dialog = self.levels[self.active_level]['scenes'][self.active_scene]['dialog'][dialog]
        self.refresh_UI()
        x, y = root.winfo_pointerxy()
        widget = root.winfo_containing(x, y)
        widget.focus()

    def refresh_UI(self):
        self.Entries.destroy()
        self.DialogPrams.destroy()
        self.make_widgets(self.parent)

    def add_dialog(self):
        self.SavetoJson()
        self.levels[self.active_level]['dialogs'].append(
            {"line": " ",  "attarction": 0, "uniqueness": 0, "scene": int(self.active_scene), "response": 0})
        self.active_dialog = len(self.levels[self.active_level]['dialogs']) - 1
        self.levels[self.active_level]['scenes'][self.active_scene]['dialog'].append(
            self.active_dialog)
        self.save_level()
        self.refresh_UI()

    def load_levels(self):
        os.chdir('..')
        self.winfo_toplevel().title("Dialog Creator " + os.getcwd())
        for r, d, f in os.walk(os.getcwd()):
            for file in f:
                if 'level_' in file and '.json' in file and '.meta' not in file:
                    self.levels.append(json.load(open(os.path.join(r, file))))

    def save_level(self):
        loc = os.getcwd()
        for name in ['Game', 'Unfrozen Game', 'Assets', 'StreamingAssets']:
            if name not in loc:
                loc += '\\' + name
        if not os.path.exists(loc):
            os.makedirs(loc)
        outfile = open(loc + f'\\level_{(self.active_level + 1)}.json', 'w')
        outfile.seek(0)
        outfile.write(json.dumps(self.levels[self.active_level], indent=4))
        outfile.truncate()

    def parse_import(self, s):
        nl = s.count('\n')
        if(nl <= 1):
            print(f"Not enough newlines, just using basic append {nl}")
            self.levels[self.active_level]['scenes'].append({"name": s, "dialog": []})
        else:
            patterns = [r"([A-Z][\w\s,!.:“”’\-\?]+)(?:\(([-+=])charm,? ([-+=])unique[\),])[a-zA-Z\s,!.…\?\)\(]*(\d)?\)",
                        r"([A-Z][\w\s,!.:“”’\-\?]+)(?:\(([-+=]) ?([-+=])[\),])?[a-zA-Z\s,!.…\?\)\(]*(\d)?\)"]
            parts = s.split('\n')
            parts = list(filter(None, parts))
            print(parts)
            start = len(self.levels[self.active_level]['dialogs'])
            self.levels[self.active_level]['scenes'].append({"name": parts[0], "dialog": list(range(start, start + len(parts) - 1))})
            for idx, part in enumerate(parts):
                for p in patterns:
                    match = search(p, part)
                    if (match):
                        try:
                            sce = match.group(4) - 1 if match.group(4) else 0
                        except TypeError:
                            sce = 0
                        self.levels[self.active_level]['dialogs'].append({
                            "line": match.group(1),
                            "attarction": self.parse_symbol(match.group(2)) or 0,
                            "uniqueness": self.parse_symbol(match.group(3)) or 0,
                            "scene": sce,
                            "response": 0
                        })
                        break
                    elif (idx > 0 and p == patterns[-1]):
                        self.levels[self.active_level]['dialogs'].append({
                            "line": part,
                            "attarction": 0,
                            "uniqueness": 0,
                            "scene": 0,
                            "response": 0
                        })

    def parse_symbol(self, symbol):
        if(symbol == "+"):
            return 1
        if(symbol == "="):
            return 0
        if(symbol == "-"):
            return -1

    def handleReturn(self, event):
        self.SavetoJson()
        self.save_level()
        self.refresh_UI()
        # print("return: event.widget is", event.widget)


if __name__ == "__main__":
    root = Tk()
    w, h = root.winfo_screenwidth(), root.winfo_screenheight()
    Main = DialogCreator(root)
    root.geometry(f"{w-200}x{h-80}+0+0")
    Grid.columnconfigure(root, 0, weight=1)
    root.mainloop()
# root.state('zoomed')
