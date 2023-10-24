import EstruturaOperacoes from '@/config/EstruturaOperacoes.json'

const getRouterData = {

    computed: {
        getRouterData() {
            const routerName = this.$route.name;
            let i = 0;
            let itemAtual;

            for (i = 0; i < EstruturaOperacoes.blocos[0].itens.length; i++) {
                itemAtual = EstruturaOperacoes.blocos[0].itens[i].router;

                if (itemAtual === routerName) {
                return EstruturaOperacoes.blocos[0].itens[i].params;
                }
            }

            for (i = 0; i < EstruturaOperacoes.blocos[1].itens.length; i++) {
                itemAtual = EstruturaOperacoes.blocos[1].itens[i].router;

                if (itemAtual === routerName) {
                return EstruturaOperacoes.blocos[1].itens[i].params;
                }
            }

            for (i = 0; i < EstruturaOperacoes.blocos[2].itens.length; i++) {
                itemAtual = EstruturaOperacoes.blocos[2].itens[i].router;

                if (itemAtual === routerName) {
                return EstruturaOperacoes.blocos[2].itens[i].params;
                }
            }

        },
    }
}

export default getRouterData
