class DataSource {
    public static APIPATH = "/api";
    private _authInfo: AuthResponse;

    public authenticate(email: string, password: string): Promise<AuthResponse> {
        return new Promise<AuthResponse>((resolve: (result: AuthResponse) => void, reject: (error) => void) => {
            JsonRequest.httpPost<AuthResponse>('/Token', { Username: email, Password: password, grant_type: "password" }).then((success) => {
                this._authInfo = success;
                resolve(success);
            },(error) => {
                    reject(error);
                });
        });
    }
}