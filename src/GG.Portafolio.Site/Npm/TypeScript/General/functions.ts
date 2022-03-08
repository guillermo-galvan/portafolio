export function changeCaracter(text: string) : string {
    let result = '';
    for (var i = 0; i < text.length; i++) {
        let char = text.charAt(i);
        result += char.replace("<", "&lt;").replace(">", "&gt;").replace('"', "&quot;");
    }

    return result;
}

global.changeCaracter = changeCaracter;

