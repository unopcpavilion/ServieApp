import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { BuildConfig } from '../models/common';

@Injectable()
export class ConfigurationService {

    private buildConfig: BuildConfig = new BuildConfig();

    constructor(
        private readonly http: HttpClient,
        private readonly title: Title
    ) { }

    public loadConfig() {
        // return new Promise((resolve, reject) => {
        //     this.http.get<BuildConfig>('/config').subscribe((res: BuildConfig) => {
        //         this.buildConfig = res;
        //         this.title.setTitle(res.companyName);
        //         resolve(true);
        //     }, error => {
        //         console.error('Cannot load app config. App can\'t start');
        //         resolve(false);
        //     });
        // });
        return this.buildConfig.apiUrl = 'https://localhost:5001/';
    }

    public getApiUrl(): string {
        return this.buildConfig.apiUrl;
    }

    public getCompanyName(): string {
        return this.buildConfig.companyName;
    }
}
