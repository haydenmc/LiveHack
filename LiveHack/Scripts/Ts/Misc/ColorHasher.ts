class ColorHasher {
    public static guidToColor(guid: string): string {
        if (!guid) {
            return 'hsl(0, 100%, 70%)';
        }
        guid = guid.replace("-", "");
        var color = Math.abs((<any>guid).hashCode()) % 360;
        return 'hsl(' + color + ', 100%, 70%)';
    }
}

/* String hashcode function */
(<any>String).prototype.hashCode = function () {
    var hash = 0;
    if (this.length == 0) return hash;
    for (var i = 0; i < this.length; i++) {
        var char = this.charCodeAt(i);
        hash += char;
    }
    return hash;
}