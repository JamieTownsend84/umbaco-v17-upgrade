import { UmbUfmFilterBase } from '@umbraco-cms/backoffice/ufm';

class UmbUfmParseJsonValueFilterApi extends UmbUfmFilterBase {
    filter(value, index = 0, property = 'Name') {
        if (!value) return '';
        try {
            const parsed = JSON.parse(value);
            const idx = parseInt(index, 10);
            if (Array.isArray(parsed)) {
                return parsed[idx]?.[property] ?? '';
            }
            if (parsed && typeof parsed === 'object') {
                return parsed[property] ?? '';
            }
            return '';
        } catch {
            return '';
        }
    }
}

export { UmbUfmParseJsonValueFilterApi as api };