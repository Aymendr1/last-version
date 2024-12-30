let cart = JSON.parse(localStorage.getItem('cart')) || [];
const cartItemsContainer = document.getElementById('cart-items');
const cartTotalElement = document.getElementById('cart-total');

// Met à jour le localStorage
function saveCartToLocalStorage() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

// Ajouter au panier
document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', () => {
        const id = button.getAttribute('data-id');
        const nom = button.getAttribute('data-nom');
        const prix = parseFloat(button.getAttribute('data-prix'));

        if (!id || !nom || isNaN(prix)) {
            console.error('Produit invalide : ', { id, nom, prix });
            return;
        }

        const existingItem = cart.find(item => item.id == id);
        if (existingItem) {
            existingItem.quantite += 1;
            existingItem.total += prix;
        } else {
            cart.push({ id, nom, prix, quantite: 1, total: prix });
        }

        saveCartToLocalStorage();
        updateCartUI();
    });
});

// Met à jour l'interface utilisateur du panier
function updateCartUI() {
    cartItemsContainer.innerHTML = '';
    let total = 0;

    cart.forEach(item => {
        total += item.total;
        const row = `
            <tr>
                <td style="padding: 15px;">${item.nom}</td>
                <td style="padding: 15px;">${item.quantite}</td>
                <td style="padding: 15px;" class="total">${item.total.toFixed(2)}</td>
                <td style="padding: 15px;">
                    <button class="btn btn-sm btn-outline-success" onclick="increaseQuantity('${item.id}')">+</button>
                    <button class="btn btn-sm btn-outline-danger" onclick="decreaseQuantity('${item.id}')">-</button>
                </td>
            </tr>
        `;
        cartItemsContainer.innerHTML += row;
    });

    cartTotalElement.textContent = total.toFixed(2);
}

// Augmenter la quantité
function increaseQuantity(id) {
    const item = cart.find(item => item.id == id);
    if (item) {
        item.quantite += 1;
        item.total += item.prix;
        saveCartToLocalStorage();
        updateCartUI();
    }
}

// Diminuer la quantité
function decreaseQuantity(id) {
    const item = cart.find(item => item.id == id);
    if (item) {
        item.quantite -= 1;
        item.total -= item.prix;
        if (item.quantite <= 0) {
            cart = cart.filter(cartItem => cartItem.id != id);
        }
        saveCartToLocalStorage();
        updateCartUI();
    }
}

// Charger le panier depuis le localStorage au chargement de la page
document.addEventListener('DOMContentLoaded', () => {
    updateCartUI();
<<<<<<< HEAD
});
=======
});
>>>>>>> 73c769160a587de48028432688bf78a640b1f9f1
