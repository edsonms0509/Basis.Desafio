window.selectInput = (element) => {
    element.select();
};

window.ShowToast = (message) => {
    $.toast({
        heading: 'AVISO',
        text: message,
        position: 'bottom-right',
        loaderBg: '#D1D1D1',
        icon: 'info',
        hideAfter: 3500,
        showHideTransition: 'slide',
        stack: 6
    });
}

window.ShowToastWarning = (message) => {
    $.toast({
        heading: 'AVISO',
        text: message,
        position: 'bottom-right',
        loaderBg: '#D1D1D1',
        icon: 'warning',
        hideAfter: 4500,
        showHideTransition: 'slide',
        stack: 6
    });
}

window.ShowModal = (id) => {
    modal = new bootstrap.Modal(document.getElementById(id));
    modal.show();
}

window.downloadFileFromStream = (filename, base64Data) => {
    const link = document.createElement('a');
    link.download = filename;
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

window.CloseModal = () => {
    if (modal && modal._isShown) {
        modal.hide();
    }
}

window.setupNumericInput = (inputId, aceitaDecimal, maxCasasDecimais, maxDigitosInteiros) => {
    const input = document.getElementById(inputId);
    if (!input) return;

    input.addEventListener('input', function (e) {
        let val = input.value;

        // Substitui ponto por vírgula
        val = val.replace(/\./g, ',');

        // Se começar com vírgula, adiciona zero à esquerda
        if (val.startsWith(',')) {
            val = '0' + val;
        }

        // Remove caracteres inválidos
        val = val.replace(/[^0-9,]/g, '');

        if (aceitaDecimal && val.includes(',')) {
            const [parteInteira, parteDecimal] = val.split(',');

            // Limita dígitos inteiros
            const intPart = parteInteira.slice(0, maxDigitosInteiros);

            // Limita casas decimais
            const decPart = parteDecimal.slice(0, maxCasasDecimais);

            val = intPart + ',' + decPart;
        } else {
            // Sem decimal: limita o total de dígitos
            val = val.slice(0, maxDigitosInteiros);
        }

        input.value = val;
    });

    input.addEventListener('keypress', function (e) {
        const char = String.fromCharCode(e.which);
        const val = input.value;

        const allowed = aceitaDecimal ? /^[0-9,]$/ : /^[0-9]$/;
        if (!allowed.test(char)) {
            e.preventDefault();
            return;
        }

        // Impede múltiplas vírgulas
        if (char === ',' && val.includes(',')) {
            e.preventDefault();
            return;
        }

        // Verifica se já atingiu o limite de inteiros
        if (aceitaDecimal) {
            const [intPart] = val.split(',');
            if (char !== ',' && intPart.length >= maxDigitosInteiros && !val.includes(',')) {
                e.preventDefault();
            }
        } else {
            if (val.length >= maxDigitosInteiros) {
                e.preventDefault();
            }
        }
    });
};
