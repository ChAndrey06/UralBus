const initSelectize = (targetId, inputId, valueField, searchField, labelField, placeholder, loadUrl, initialOption, requestData) => {
    return $(`#${targetId}`).selectize({
        valueField,
        searchField,
        labelField: typeof labelField == 'string' ? labelField : null,
        create: false,
        render: typeof labelField == 'function' ? {
            item: (item, escape) => {
                return '<div>' + escape(labelField(item)) + '</div>';
            },
            option: (item, escape) => {
                return '<div>' + escape(labelField(item)) + '</div>';
            }
        } : null,
        placeholder,
        load: (query, callback) => {
            $.ajax({
                url: loadUrl,
                type: 'GET',
                dataType: 'json',
                data: {
                    searchQuery: query,
                    ...requestData
                },
                error: () => {
                    callback();
                },
                success: (res) => {
                    callback(res.items);
                }
            });
        },
        onChange: value => {
            $(`#${inputId}`).val(value);
        },
        onInitialize: function () {
            if (!initialOption || !initialOption[valueField]) return;

            this.addOption(initialOption);
            this.setValue(initialOption[valueField]);
        }
    });
}