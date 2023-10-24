export default {
    namespaced: true,
    state: {
        loading: true,
        filter: false,
        search: null,
        active: null,
    },
    mutations: {
        SET_LOADING(state) {
            state.loading = true
        },
        UNSET_LOADING(state) {
            state.loading = false
        },

        SET_SEARCH(state, payload) {
            state.search = payload
        },

    },
    actions: {
        SET_SEARCH(context, payload) {
            context.commit('SET_SEARCH', payload)
        },
    }
}
